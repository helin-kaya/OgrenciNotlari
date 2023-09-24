using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OgrenciNotlariBL.InterfaceofManagers;
using OgrenciNotlariDL.InterfaceofRepos;
using OgrenciNotlariEL.ResultModels;

namespace OgrenciNotlariBL.ImplementationofManagers
{
    public class Manager<TDTO, TModel, Tid> : IManager<TDTO, Tid>
        where TDTO : class, new()
        where TModel : class, new()
    {
        protected readonly IRepository<TModel, Tid> _repo;
        protected readonly IMapper _mapper;
        protected readonly string[] _joinTablesName;
        public Manager(IRepository<TModel, Tid> repo, IMapper mapper, string[] joinTablesName)
        {
            _repo = repo;
            _mapper = mapper;
            _joinTablesName = joinTablesName;
        }

        public IDataResult<TDTO> Add(TDTO entity)
        {
            try
            {
                //Ekleme işlemi repoda yapılıyor
                var data = _mapper.Map<TDTO, TModel>(entity);

                if (_repo.Add(data) > 0)
                {
                    var sonuc = new DataResult<TDTO>();
                    sonuc.IsSuccess = true;
                    sonuc.Message = "Ekleme başarıldır!";
                    sonuc.Data = _mapper.Map<TModel, TDTO>(data);
                    return sonuc;
                }
                else
                {
                    return new DataResult<TDTO>(false, "Ekleme işlemi başarısızdır! Tekrar deneyiniz!", null);
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IResult Delete(TDTO entity)
        {
            try
            {
                var data = _mapper.Map<TDTO, TModel>(entity);
                if (_repo.Delete(data) > 0)
                {

                    var gonderilecekData = _mapper.Map<TModel, TDTO>(data);
                    return new DataResult<TDTO>(true, "Silme işlemi başarılıdır!", gonderilecekData);
                }
                else
                {
                    var sonuc = new DataResult<TDTO>();
                    sonuc.IsSuccess = false;
                    sonuc.Message = "Silme işlemi başarısız oldu! Tekrar deneyiniz!";
                    sonuc.Data = null;
                    return sonuc;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IDataResult<ICollection<TDTO>> GetAll(Expression<Func<TDTO, bool>>? whereFilter = null, string[]? joinTablesName = null)
        {
            try
            {
                if (_joinTablesName != null)
                {
                    joinTablesName = _joinTablesName;
                }

                var allData = _repo.GetAll(_mapper.Map<Expression<Func<TDTO, bool>>,
                    Expression<Func<TModel, bool>>>(whereFilter), joinTablesName);

                if (allData == null)
                {
                    return new DataResult<ICollection<TDTO>>(false, "Veriler bulunamadı!", null);
                }
                else
                {
                    var sonuc = _mapper.Map<IQueryable<TModel>, ICollection<TDTO>>(allData);
                    return new DataResult<ICollection<TDTO>>(true, $"{sonuc.Count} adet veri bulundu!", sonuc);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IDataResult<TDTO> GetByCondition(Expression<Func<TDTO, bool>>? whereFilter = null, string[]? joinTablesName = null)
        {
            try
            {

                if (_joinTablesName != null)
                {
                    joinTablesName = _joinTablesName;
                }
                var filter = _mapper.Map<Expression<Func<TDTO, bool>>,
                    Expression<Func<TModel, bool>>>(whereFilter);
                var data = _repo.GetByCondition(filter, joinTablesName);

                return data == null ?
                    new DataResult<TDTO>(false, "", null)
                    :
                    new DataResult<TDTO>(true, "", _mapper.Map<TModel, TDTO>(data))
                    ;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public IDataResult<TDTO> GetbyId(Tid id)
        {
            try
            {
                var data = _repo.GetById(id);
                if (data != null)
                {
                    return new DataResult<TDTO>(true, "Veri Bulundu!", _mapper.Map<TModel, TDTO>(data));
                }
                else
                {
                    return new DataResult<TDTO>(false, "Veri Bulunamadı!", null);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IDataResult<TDTO> Update(TDTO entity)
        {
            try
            {
                var data = _mapper.Map<TDTO, TModel>(entity);
                return _repo.Update(data) > 0 ?
                     new DataResult<TDTO>(true, "Güncelleme başarıldır!", _mapper.Map<TModel, TDTO>(data))
                     :
                      new DataResult<TDTO>(false, "Güncelleme işleminde hata oluştu! Tekrar deneyiniz!", null);
                ;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    
}
