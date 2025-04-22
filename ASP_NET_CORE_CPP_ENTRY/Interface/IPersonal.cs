using Pruebas.Cliente.Models;

namespace Pruebas.Cliente.Interface
{
    public interface IPersonal
    {
        public List<Personal> ObtenerPersonal(string? nombre);

        public int BorrarPersonal(int id);

        //public TblProducto ObtenerProductos(int idProducto);

        //public TblProducto ActualizarProducto(TblProducto pro);

        //public TblProducto AgregarProducto(TblProducto pro);

        //public TblProducto EliminarProducto(int idProducto);
    }
}
