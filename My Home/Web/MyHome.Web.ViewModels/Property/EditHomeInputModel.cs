using AutoMapper;
using FindPlace.Data.Models;
using Microsoft.AspNetCore.Http;
using MyHome.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyHome.Web.ViewModels.Property
{
    public class EditHomeInputModel : BasePropertyInputModel, IMapFrom<Home>
    {
    }
}
