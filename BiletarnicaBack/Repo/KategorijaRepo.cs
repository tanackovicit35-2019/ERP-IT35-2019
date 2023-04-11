using AutoMapper;
using BiletarnicaBack.Entities;

namespace BiletarnicaBack.Repo
{
    public class KategorijaRepo : IKategorijaRepo
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public KategorijaRepo(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<KategorijaEntity> GetKategorija()
        {
            return context.kategorija.ToList();
        }

        public KategorijaEntity GetKategorijaByID(int kategorijaID)
        {
            return context.kategorija.FirstOrDefault(r => r.kategorijaID == kategorijaID);
        }



        public KategorijaEntity CreateKategorija(KategorijaEntity kategorijaEntity)
        {
            var created = context.Add(kategorijaEntity);
            return mapper.Map<KategorijaEntity>(created.Entity);
        }

        public void UpdateKategorija(KategorijaEntity kategorijaEntity)
        {
            //entity framework
        }

        public void DeleteKategorija(int kategorijaID)
        {
            var kategorija = GetKategorijaByID(kategorijaID);
            context.Remove(kategorija);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
