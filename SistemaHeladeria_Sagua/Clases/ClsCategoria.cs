using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaHeladeria_Sagua.DataClases;
using SistemaHeladeria_Sagua.Clases;

namespace SistemaHeladeria_Sagua.Clases
{
    public class ClsCategoria
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        public List<Categoria> Listar()
        {
            List<Categoria> listarCategoria = dc.Categoria.ToList();
            return listarCategoria;
        }

        public Categoria Obtener(int id) //Retornar un solo objeto
        {
            Categoria objeto = new Categoria();
            return objeto;
        }

        public void Agregar(Categoria objCategoria)
        {
            dc.Categoria.InsertOnSubmit(objCategoria);
            dc.SubmitChanges();
        }

        public void Actualizar(Categoria categoria)
        {
            Categoria objcate = dc.Categoria.Single(c => c.id_categ == categoria.id_categ);

            objcate.nombre = categoria.nombre;
            objcate.descripcion = categoria.descripcion;
            objcate.estado = categoria.estado;

            dc.SubmitChanges();
        }



        public void Eliminar(int id)
        {
            Categoria objCategoria = dc.Categoria.Single(c => c.id_categ == id);
            dc.Categoria.DeleteOnSubmit(objCategoria);
            dc.SubmitChanges();
        }
    }
}




