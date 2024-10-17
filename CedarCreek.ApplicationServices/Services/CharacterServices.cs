using CedarCreek.Core.Domain;
using CedarCreek.Core.ServiceInterface;
using CedarCreek.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedarCreek.ApplicationServices.Services
{
	public class CharacterServices : ICharactersServices
	{
		private readonly CedarCreekContext _context;

        public CharacterServices(CedarCreekContext context)
        {
            _context = context;
            //_fileServices = fileServices
        }
        public async Task<Character> DetailsAsync(Guid id)
        {
            var result = await _context.Characters
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }
    }
	
}
