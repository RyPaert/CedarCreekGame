using CedarCreek.Core.Domain;
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
	}
}
