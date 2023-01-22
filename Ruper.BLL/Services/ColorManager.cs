﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Ruper.BLL.Data;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.Base;
using Ruper.DAL.DataContext;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories;

namespace Ruper.BLL.Services
{
    public class ColorManager : EfCoreRepository<Color> , IColorService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ColorManager(AppDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public override async Task AddAsync(Color entity)
        {
            var color = await _dbContext.Colors
                .Where(x => x.ColorName == entity.ColorName || x.ColorCode == entity.ColorCode)
                .FirstOrDefaultAsync();  

            if(color is not null) throw new Exception();

            await base.AddAsync(entity);
        }

        public override async Task CompletelyDeleteAsync(int? id)
        {
            if (id is null) throw new Exception();

            var deletedEntity = await _dbContext.Colors.FindAsync(id);

            if (deletedEntity is null) throw new Exception();

            var productColor = await _dbContext.ProductColors
                               .Where(x => x.ColorId == deletedEntity.Id).
                               FirstOrDefaultAsync();

            if (productColor is not null) throw new Exception();

            _dbContext.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateById(int? id, ColorUpdateDto colorUpdateDto)
        {
            if (id is null) throw new Exception();

            var existColor = await _dbContext.Colors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existColor is null) throw new Exception();

            if (colorUpdateDto.Id != id) throw new Exception();

            if (colorUpdateDto.ColorName is null)
            {
                colorUpdateDto.ColorName = existColor.ColorName;
            }
            else 
            {
                if (colorUpdateDto.ColorName.Trim() != existColor.ColorName.Trim())
                {
                    var sameColor = await _dbContext.Colors
                    .Where(x => x.ColorName == colorUpdateDto.ColorName || x.ColorCode == colorUpdateDto.ColorCode)
                    .FirstOrDefaultAsync();

                    if (sameColor is not null) throw new Exception();
                }                
            }

            if (colorUpdateDto.ColorCode is null) colorUpdateDto.ColorCode = existColor.ColorCode;            

            if (colorUpdateDto.IsDeleted is null)
            {
                colorUpdateDto.IsDeleted = existColor.IsDeleted;
            }
            else
            {
                var productColor = await _dbContext.ProductColors
                    .Where(x => x.ColorId == id && x.IsDeleted == false)
                    .FirstOrDefaultAsync();

                if (colorUpdateDto.IsDeleted == true && productColor != null) throw new Exception();
            }

            var color = _mapper.Map<Color>(colorUpdateDto);

            _dbContext.Colors.Update(color);
            await _dbContext.SaveChangesAsync();
        }
    }
}
