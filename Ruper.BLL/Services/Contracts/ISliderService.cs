using Microsoft.AspNetCore.Mvc;
using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruper.BLL.Services.Contracts
{
    public interface ISliderService : IRepository<Slider>
    {
        Task UpdateById(int? id, SliderUpdateDto sliderUpdateDto);
    }
}
