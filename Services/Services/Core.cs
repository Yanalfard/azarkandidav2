using DataLayer;
using Services.Repositories;

namespace Services.Services
{
    public class Core
    {
        private readonly AzarkandidaV2Entities _db = new AzarkandidaV2Entities();

        private MainRepo<TblImage> _image;
        private MainRepo<TblBlog> _blog;

        public MainRepo<TblImage> Image => _image ?? (_image = new MainRepo<TblImage>(_db));
        public MainRepo<TblBlog> Blog => _blog ?? (_blog = new MainRepo<TblBlog>(_db));

        public void Save() => _db.SaveChanges();
    }
}
