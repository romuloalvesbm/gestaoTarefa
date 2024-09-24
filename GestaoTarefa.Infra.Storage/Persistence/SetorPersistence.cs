using GestaoTarefa.Infra.Storage.Collections;
using GestaoTarefa.Infra.Storage.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoTarefa.Infra.Storage.Persistence
{
    public class SetorPersistence
    {
        private readonly MongoDBContext _mongoDBContext;

        public SetorPersistence(MongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext;
        }

        public async Task Insert(SetorCollection setor)
        {
            await _mongoDBContext.Setor.InsertOneAsync(setor);
        }

        public async Task Update(SetorCollection setor)
        {
            var filter = Builders<SetorCollection>.Filter.Eq(t => t.SetorId, setor.SetorId);
            await _mongoDBContext.Setor.ReplaceOneAsync(filter, setor);
        }

        public async Task Delete(SetorCollection setor)
        {
            var filter = Builders<SetorCollection>.Filter.Eq(t => t.SetorId, setor.SetorId);
            await _mongoDBContext.Setor.DeleteOneAsync(filter);
        }

        public async Task<List<SetorCollection>> FindAll()
        {
            var filter = Builders<SetorCollection>.Filter.Where(t => true);
            var result = await _mongoDBContext.Setor.FindAsync(filter);
            return result.ToList();
        }

        public async Task<SetorCollection> Find(Guid id)
        {
            var filter = Builders<SetorCollection>.Filter.Eq(t => t.SetorId, id);
            var result = await _mongoDBContext.Setor.FindAsync(filter);
            return result.FirstOrDefault();
        }
    }
}
