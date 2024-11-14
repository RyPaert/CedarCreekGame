using CedarCreek.Core.Domain;
using CedarCreek.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.Core.ServiceInterface
{
	public interface ICharactersServices
	{
		Task<Character> DetailsAsync(Guid id);

		Task<Character> Create(CharacterDto dto);
		Task<Character> Update(CharacterDto dto);
		Task<Character> Delete(Guid id);
	}
}
