/*
 * Creado por SharpDevelop.
 * Usuario: Usuario
 * Fecha: 13/03/2010
 * Hora: 07:02 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Formulacion_del_nectar
{
	/// <summary>
	/// Description of FrmAcercaDe.
	/// </summary>
	public partial class FrmAcercaDe : Form
	{
		public FrmAcercaDe()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			lnkAcercaDe.Links[0].LinkData="aqui";
			lnkAcercaDe.Links.Add(206,4,"http://www.gnu.org/copyleft/gpl.html");
			lnkAcercaDe.Links.Add(234,4,"http://www.viti.es/gnu/licenses/gpl.html");
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void LnkAcercaDeLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string objetivo=Convert.ToString(e.Link.LinkData);
			if (objetivo.StartsWith("http://"))
				{
            	System.Diagnostics.Process.Start(objetivo);
        		}
		}
	}
}
