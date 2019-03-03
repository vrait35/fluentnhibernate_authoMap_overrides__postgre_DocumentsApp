using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DbContextLibrary.PostgreSql;

namespace DosumentsApp.Models
{
    public class FileViewModel
    {      

       [Required,Display(Name ="Введите имя документа")]
       public string DocumentName { get; set; }

       [Required,DataType(DataType.DateTime),Display(Name ="Введите дату создания документа")]
       public DateTime DateTimeCreateDocument { get; set; }
       
      [Display(Name ="Выберите файлы")]
       public List< IFormFile> Files { get; set; }       

       [Required,EnumDataType(typeof(FileTypeEnum))]
       public  List< FileTypeEnum> FileTypes { get; set; }
       
    }
}
