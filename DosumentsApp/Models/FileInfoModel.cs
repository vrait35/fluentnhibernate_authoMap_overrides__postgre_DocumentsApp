using DbContextLibrary.PostgreSql;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DosumentsApp.Models
{
    public class FileInfoModel
    {
        [Required]
        public IFormFile Files { get; set; }
                
        [Required,EnumDataType(typeof(FileTypeEnum))]
        public FileTypeEnum DocumentType { get; set; }

    }
}
