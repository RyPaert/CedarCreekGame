using CedarCreek.Core.Domain;
using CedarCreek.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(CharacterDto dto, Character realm);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
    }
}
