﻿using System.Collections.Generic;
using ToDo.Business.Interfaces;
using ToDo.DataAccess.Interfaces;
using ToDo.Entities.Concrete;

namespace ToDo.Business.Concrete
{
    public class BildirimManager : IBildirimService
    {
        private readonly IBildirimDal _bildirimDal;

        public BildirimManager(IBildirimDal bildirimDal)
        {
            _bildirimDal = bildirimDal;
        }

        public List<Bildirim> GetirHepsi()
        {
            return _bildirimDal.GetirHepsi();
        }

        public Bildirim GetirId(int id)
        {
            return _bildirimDal.GetirId(id);
        }

        public void Guncelle(Bildirim model)
        {
            _bildirimDal.Guncelle(model);
        }

        public void Kaydet(Bildirim model)
        {
            _bildirimDal.Kaydet(model);
        }

        public void Sil(Bildirim model)
        {
            _bildirimDal.Sil(model);
        }
    }
}