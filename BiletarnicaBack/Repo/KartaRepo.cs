using AutoMapper;
using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public class KartaRepo : IKartaRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public KartaRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<KartaEntity> GetKarta()
        {
            return context.karta.ToList();
        }

        public KartaEntity GetKartaByID(int kartaID)
        {
            return context.karta.FirstOrDefault(r => r.kartaID == kartaID);
        }



        public KartaEntity CreateKarta(KartaEntity kartaEntity)
        {
            var created = context.Add(kartaEntity);
            return mapper.Map<KartaEntity>(created.Entity);
        }

        public void UpdateKarta(KartaEntity kartaEntity)
        {
            //entity framework
        }

        public void DeleteKarta(int kartaID)
        {
            var karta = GetKartaByID(kartaID);
            context.Remove(karta);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
