using Microsoft.EntityFrameworkCore;
using P01_ArenaMvc.DataAccess;
using P01_ArenaMvc.DTO;
using P01_ArenaMvc.Entity;
using P01_ArenaMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Repositories
{
    public class FighterRepository
    {
        private readonly AppDbCtx _db;

        public FighterRepository(AppDbCtx db) {
            _db = db;
        }

        public Task<List<FighterEntity>> GetList() {
            IQueryable<FighterEntity> queryable = _db.Fighters;
            return  queryable.ToListAsync();
        }

        public async Task AddFighter(FighterEntity fighter) {
            var entity = new FighterEntity {
                Id = default,
                EpicName = fighter.EpicName,
                Speed = fighter.Speed,
                PV = fighter.PV,
                Class = fighter.Class,
                Damage = fighter.Damage
            };
            _db.Fighters.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFighter(int id) {
            var model = await _db.Fighters.FindAsync(id);
            if (model != null) {
                _db.Fighters.Remove(model);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateFighter(FighterDTO fighter, int id) {
            var updatedFighter = _db.Fighters.Where(f => f.Id == id).Single();
            updatedFighter.EpicName = fighter.EpicName;
            updatedFighter.PV = fighter.PV;
            updatedFighter.Speed = fighter.Speed;
            updatedFighter.Class = fighter.Class;
            updatedFighter.Damage = fighter.Damage;
            await _db.SaveChangesAsync();
        }
    }
}
