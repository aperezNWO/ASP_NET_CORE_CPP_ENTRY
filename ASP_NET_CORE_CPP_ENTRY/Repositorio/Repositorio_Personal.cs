using Microsoft.EntityFrameworkCore;
using Pruebas.Cliente.Interface;
using Pruebas.Cliente.Models;

namespace Pruebas.Cliente.Repositorio
{
    public class Repositorio_Personal : IPersonal
    {
        private readonly SigaFfmContext _dbContext;

        public Repositorio_Personal(SigaFfmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Personal> ObtenerPersonal(string? nombre)
        {
            try
            {
                return _dbContext.Personals.Where(x => x.PNombre.Contains(nombre) && (x.Estado==true)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int BorrarPersonal(int id)
        {
            try
            {
                return _dbContext.Personals.Where(u => u.Id == id)
                            .ExecuteUpdate(u => u.SetProperty(p => p.Estado, false));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
       
                public TblProducto ActualizarProducto(TblProducto pro)
                {
                    try
                    {
                        _dbContext.Entry(pro).State = EntityState.Modified;

                        _dbContext.Update(pro);
                        _dbContext.SaveChanges();

                        return pro;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                public TblProducto AgregarProducto(TblProducto pro)
                {
                    try
                    {
                        _dbContext.Entry(pro).State = EntityState.Added;

                        _dbContext.Add(pro);
                        _dbContext.SaveChanges();

                        return pro;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                public TblProducto EliminarProducto(int idProducto)
                {
                    try
                    {
                        var pro = _dbContext.TblProductos.SingleOrDefault(x => x.IdProducto == idProducto);

                        _dbContext.Entry(pro).State = EntityState.Deleted;

                        _dbContext.Remove(pro);
                        _dbContext.SaveChanges();

                        return pro;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            */
    }
}