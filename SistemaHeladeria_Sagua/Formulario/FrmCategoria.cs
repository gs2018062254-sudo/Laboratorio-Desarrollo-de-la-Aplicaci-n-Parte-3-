using SistemaHeladeria_Sagua.Clases;
using SistemaHeladeria_Sagua.DataClases;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaHeladeria_Sagua;

namespace SistemaHeladeria_Sagua.Formulario
{
    public partial class FrmCategoria : Form
    {

        //instanciar
        private static int id = 0;
        private ClsCategoria objCategoria = new ClsCategoria();
        DataClasses1DataContext dc = new DataClasses1DataContext();

        public FrmCategoria()
        {
            InitializeComponent();
            CargarComboEstado();
            Listar3();
            txtId.Enabled = false;
            DesactivarCampos();
            ActivarBotones(true, false, false, false, true);
        
        }
        public void Listar()
        {
            dgvCategorias.AutoGenerateColumns = false;
            dgvCategorias.DataSource = objCategoria.Listar();
        }
        public void Listar2()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var categorias = (from x in dc.Categoria select x).ToList();
            dgvCategorias.DataSource = categorias;
        }

        private void Listar3()
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            var categorias = (from c in dc.Categoria
                              select new
                              {
                                  c.id_categ,
                                  c.nombre,
                                  c.descripcion,
                                  Estado = c.estado.Equals("A") ?
                                  "Activo" : "Desconocido"
                              }).ToList();
            dgvCategorias.DataSource = categorias;
        }
        public void CargarComboEstado()
        {
            cmbEstado.Items.Clear();
            //cmbEstado.Items.Add("A");
            //cmbEstado.Items.Add("I");

            //Mostrar un texto personalizado y el valor interno 
            var estados = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("A", "Activo"),
                new KeyValuePair<string, string>("I", "Inactivo")
            };

            cmbEstado.DataSource = estados;
            cmbEstado.DisplayMember = "Value";
            cmbEstado.ValueMember = "Key";
        }

        public void ActivarBotones(bool n, bool a, bool m, bool e, bool c)
        {
            btnNuevo.Enabled = n;
            btnGrabar.Enabled = a;
            btnModificar.Enabled = m;
            btnEliminar.Enabled = e;
            btnCancelar.Enabled = c;
        }


        public void ActivarCampos()
        {
            txtNombre.Enabled = true;
            txtDescripcion.Enabled = true;
            cmbEstado.Enabled = true;
        }

        public void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            id = 0;
        }
        public void DesactivarCampos()
        {

        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {

        }
        public void Obtener(int id)
        {
            Categoria objCateg = objCategoria.Obtener(id);
            txtNombre.Text = objCateg.nombre.ToString();
            txtDescripcion.Text = objCateg.descripcion.ToString();
            cmbEstado.Text = objCateg.estado.ToString();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                Categoria objCateg = new Categoria();
                objCateg.id_categ = id;
                objCateg.nombre = txtNombre.Text;
                objCateg.descripcion = txtDescripcion.Text;

                if (cmbEstado.SelectedValue != null)
                {
                    objCateg.estado = Convert.ToChar(cmbEstado.SelectedValue);
                }
                else
                {
                    MessageBox.Show("Seleccioe un estado Valido...", "Sistema Heladeria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                objCategoria.Actualizar(objCateg);

                Limpiar();
                ActivarBotones(true, false, false, false, true);
                DesactivarCampos();
            }
        }

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dgvCategorias.Rows)
            {
                if (row.Index == e.RowIndex)
                {
                    id = int.Parse(row.Cells[0].Value.ToString());
                    txtNombre.Text = row.Cells[1].Value.ToString();


                    if (row.Cells[2].Value.ToString() != null)
                    {
                        txtDescripcion.Text = row.Cells
                            [2].Value.ToString();
                    }

                    cmbEstado.Text = Convert.ToChar(row.Cells[3].Value).ToString();

                    objCategoria.Obtener(id);
                    ActivarBotones(false, false, true, true, true);
                    ActivarCampos();
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ActivarCampos();
            ActivarBotones(false, true, false, false, true);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Estas seguro de eliminar el registro...?", "Sistema Heladeria", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (id != 0)
                {
                    objCategoria.Eliminar(id);
                    MessageBox.Show("Registro eliminado con exito...", "Sistema Heladeria",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Limpiar();
                    Listar3();
                    DesactivarCampos();
                    ActivarBotones(true, false, false, false, true);
                }
            }
        }
    }
}
