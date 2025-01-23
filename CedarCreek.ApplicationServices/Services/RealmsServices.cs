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
	public class RealmsServices : IRealmsServices
	{
		private readonly CedarCreekContext _context;
		private readonly IRealmsServices _realmsServices;
		private readonly IFileServices _fileServices;

		public RealmsServices(CedarCreekContext context, IFileServices fileServices)
		{
			_context = context;
			_fileServices = fileServices;
		}

		public async Task<Realm> DetailsAsync(Guid id)
		{
			var result = await _context.Realms
				.FirstOrDefaultAsync(x => x.ID == id);
			return result;
		}
		public async Task<Realm> Create (RealmDto dto)
		{
			Realm realm = new();

			realm.ID = Guid.NewGuid();
			realm.RealmName = dto.RealmName;
			realm.RealmEffect = (Core.Domain.RealmEffect)dto.RealmEffect;
			realm.CharacterLevelRequirement = dto.CharacterLevelRequirement;

			await _context.Realms.AddAsync(realm);
			await _context.SaveChangesAsync();

			return realm;
		}
		public async Task<Realm> Delete (Guid id)
		{
			var result = await _context.Realms.FirstOrDefaultAsync(x => x.ID == id);
			_context.Realms.Remove(result);
			await _context.SaveChangesAsync();
			return result;
		}
	}
}
