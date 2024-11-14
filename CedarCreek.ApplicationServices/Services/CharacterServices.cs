using CedarCreek.Core.Domain;
using CedarCreek.Core.Dto;
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
        private readonly IFileServices _fileServices;

        public CharacterServices(CedarCreekContext context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
            _fileServices = fileServices;
        }
        public async Task<Character> DetailsAsync(Guid id)
        {
            var result = await _context.Characters
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }
    public async Task<Character> Create(CharacterDto dto)
        {
            Character character = new();

            //Set by service
            character.ID = Guid.NewGuid();
            character.CharacterHealth = 100;
            character.CharacterLevel = 0;
            character.CharacterXP = 0;
            character.CharacterXPNextLevel = 100;
            character.CharacterLevel = 0;
            character.CharacterStatus = Core.Domain.CharacterStatus.Healthy;
            character.CreatedAt = DateTime.Now;


            //Set by user
            character.CharacterClass = (Core.Domain.CharacterClass)dto.CharacterClass;
            character.PrimaryAttackName = dto.PrimaryAttackName;
            character.PrimaryAttackPower = dto.PrimaryAttackPower;
            character.SpecialAttackName = dto.SpecialAttackName;
            character.SpecialAttackPower = dto.SpecialAttackPower;

            //set for db
            character.CreatedAt = DateTime.Now;
            character.UpdatedAt = DateTime.Now;

            //files
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, character);
            }
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();

            return character;
        }
        public async Task<Character> Delete(Guid id)
        {
            var result = await _context.Characters
                .FirstOrDefaultAsync(x => x.ID == id);
            _context.Characters.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
