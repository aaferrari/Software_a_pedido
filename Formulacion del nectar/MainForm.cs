/*
 * Creado por SharpDevelop.
 * Usuario: Usuario
 * Fecha: 01/03/2010
 * Hora: 07:44 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Formulacion_del_nectar
{
	/// <summary>
	/// La informacion para hacer los calculos en este programa provino de http://www.virtual.unal.edu.co/cursos/agronomia/2006228/teoria/obnecfru/p3.htm
	/// </summary>
	public partial class FrmPrincipal : Form
	{
		bool archivo_guardado;//Sirve para determinar si los resultados fueron guardado con exito, lo cual se usara en el boton cerrar
		float acido;
			float indice_maduracion;
		public FrmPrincipal()
		{
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			
			InitializeComponent();
					
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
				
		void NudAcidoAscValueChanged(object sender, EventArgs e)
		{//El siguiente codigo comprueba que si la suma de todos los campos es mayor a 100%, mostrara una advertencia para que el usuario revise los datos
			float resultado;
			resultado=Convert.ToSingle(nudAcidoAsc.Value+nudAcidoCit.Value+nudAgua.Value+nudIsomalt.Value+nudPectina.Value+nudPulpa.Value+nudSorbato.Value+nudSuclarosa.Value);
			if (resultado > 100)
			{MessageBox.Show("La suma de los porcentajes es de mas del 100%. Por favor revise los calculos", "Aviso de porcentaje incorrecto",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
		}
		
		
		void BtnCalcularClick(object sender, EventArgs e)
		{
			acido=Convert.ToSingle(nudAcidoAsc.Value+nudAcidoCit.Value);//Esta variable es para saber cuales son los acidos que se debe incluir en los calculos (y de paso es mas facil su modificacion futura de ser necesario)
			indice_maduracion=Convert.ToSingle(txtGBrix.Text)/acido;
			Componentes Producto=new Componentes();
			Producto.AcidoAscorbico=Convert.ToSingle(nudAcidoAsc.Value);
			Producto.AcidoCitrico=Convert.ToSingle(nudAcidoCit.Value);
			Producto.Agua=Convert.ToSingle(nudAgua.Value);
			Producto.Isomalt=Convert.ToSingle(nudIsomalt.Value);
			Producto.Pectina=Convert.ToSingle(nudPectina.Value);
			Producto.Sorbato=Convert.ToSingle(nudSorbato.Value);
			Producto.Pulpa=Convert.ToSingle(nudPulpa.Value);
			Producto.Suclarosa=Convert.ToSingle(nudSuclarosa.Value);
			Producto.Nectar=Convert.ToSingle(nudKilosNectar.Value);
			txtResultados.Text="Proporcion de ingredientes en "+Convert.ToString(Producto.Nectar)+" kilos de producto\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" +
				"~~~~~\r\n"+lblAcidoAsc.Text+" "+Producto.TresSimple(Producto.AcidoAscorbico)+"\r\n"+
				lblAcidoCit.Text+" "+Producto.TresSimple(Producto.AcidoCitrico)+"\r\n"+
				lblAgua.Text+" "+Producto.TresSimple(Producto.Agua)+"\r\n"+
				lblIsomalt.Text+" "+Producto.TresSimple(Producto.Isomalt)+"\r\n"+
				lblPectina.Text+" "+Producto.TresSimple(Producto.Pectina)+"\r\n"+
				lblPulpa.Text+" "+Producto.TresSimple(Producto.Pulpa)+"\r\n"+
				lblSorbato.Text+" "+Producto.TresSimple(Producto.Sorbato)+"\r\n"+
				lblSucralosa.Text+" "+Producto.TresSimple(Producto.Suclarosa)+"\r\n";
			//Averiguacion de proporciones (dependiendo de cual textbox este completado) resolviendo el sistema de ecuaciones lineales mediante la "regla de Cramer" (mas informacion en http://es.wikipedia.org/wiki/Sistema_de_ecuaciones_lineales#Regla_de_Cramer) pero simplificado para ahorrar tiempo de procesamiento
			float resultado1;
			float resultado2;
			if (txtAcidezNecesaria.Text != "(no necesario)")
			{
				resultado1=(100-Convert.ToSingle(txtAcidezNecesaria.Text))/(1-(acido/100));
				resultado2=(Convert.ToSingle(txtAcidezNecesaria.Text)-(100*(acido/100)))/(1-(acido/100));
				txtResultados.Text+="Cuando se mezclan "+Convert.ToString(resultado1)+" partes de pulpa con "+Convert.ToString(resultado2)+" partes de ácidos se obtiene una pulpa que contiene "+Convert.ToString(acido)+"% de ácido y "+txtGBrix.Text+" Brix con un IM de "+txtIndiceMadurez.Text+".";
			}
			else
			{
				resultado1=(100-Convert.ToSingle(txtBrixNecesario.Text))/(1-(Convert.ToSingle(txtGBrix.Text)/100));
				resultado2=((Convert.ToSingle(txtBrixNecesario.Text))-(100*(Convert.ToSingle(txtGBrix.Text)/100)))/(1-(Convert.ToSingle(txtGBrix.Text)/100));
				txtResultados.Text+="Cuando se mezclan "+Convert.ToString(resultado1)+" partes de pulpa con "+Convert.ToString(resultado2)+" partes de sacarosa se obtiene una pulpa que contiene "+txtGBrix.Text+" Bx y "+Convert.ToString(acido)+" % de ácido y un IM de "+txtIndiceMadurez.Text+".";
			}
		}
		
		void NudSuclarosaValidated(object sender, EventArgs e)
		{
			txtGBrix.Text=Convert.ToString(nudIsomalt.Value+nudSuclarosa.Value);
			acido=Convert.ToSingle(nudAcidoAsc.Value+nudAcidoCit.Value);//Esta variable es para saber cuales son los acidos que se debe incluir en los calculos (y de paso es mas facil su modificacion futura de ser necesario)
			indice_maduracion=Convert.ToSingle(txtGBrix.Text)/acido;
			txtIndiceMadurez.Text=indice_maduracion.ToString();
			if (indice_maduracion > Convert.ToSingle(nudIMDeseado.Value))//Si el indice de maduracion es mayor al deseado, entonces que divida el indice de maduracion deseado por los grados Brix
			{txtAcidezNecesaria.Text=(Convert.ToSingle(nudIMDeseado.Value)/Convert.ToSingle(txtGBrix.Text)).ToString();}
			else
				{txtAcidezNecesaria.Text="(no necesario)";}
			if (indice_maduracion < Convert.ToSingle(nudIMDeseado.Value))//Si el indice de maduracion es menor al deseado, entonces que multitlique el indice de maduracion deseado porel % de acido
				{txtBrixNecesario.Text=Convert.ToString(Convert.ToSingle(nudIMDeseado.Value)*acido);}
			else
				{txtBrixNecesario.Text="(no necesario)";}
		}
		
		void BtnCopiarClick(object sender, EventArgs e)
		{
			//Funcion vista en http://foro.noticias3d.com/vbulletin/showthread.php?t=281172
			Clipboard.SetText(txtResultados.Text);
		}
		
		void BtnGuardarClick(object sender, EventArgs e)
		{
			Guardador();
		}
		
		void SalirToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (archivo_guardado == false && MessageBox.Show("¿Esta segur@ que desea salir del prograam sin haber guardado los ultimos resultados?", "¿Seguro?",MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                this.Close();
            }

			else
				{
                this.Close();
            	}
		}
		
		public void Guardador()
		{//La forma para hacer el cuadro de "Guardar como ..." la obtuve de http://msdn.microsoft.com/es-es/library/sfezx97z%28VS.80%29.aspx
		// y la forma de guardar archivos de texto la encontre en http://es.answers.yahoo.com/question/index?qid=20080921213122AAGgKXP
			SaveFileDialog CuadroGuardar=new SaveFileDialog();
			CuadroGuardar.Filter="Archivo de texto|*.txt";//Elegimos el tipo de archivo a guardar
			CuadroGuardar.Title="Guardar resultados del analisis como";//Titulo que aparecera en el cuadro de dialogo
			CuadroGuardar.FileName="Resultados "+Convert.ToString(DateTime.Today.Day+"-"+DateTime.Today.Month+"-"+DateTime.Today.Year)+".txt";//Nombre de archivo (con la fecha actual agregada)
			CuadroGuardar.ShowDialog();
			System.IO.StreamWriter EscritorArchivos=new System.IO.StreamWriter(CuadroGuardar.FileName);
			EscritorArchivos.Write(txtResultados.Text);
			EscritorArchivos.Close();
			archivo_guardado=true;
		}
		
		void GuardarResultadosComoToolStripMenuItemClick(object sender, EventArgs e)
		{
			Guardador();
		}
		
		void AcercaDeToolStripMenuItemClick(object sender, EventArgs e)
		{
			FrmAcercaDe About=new FrmAcercaDe();
			About.ShowDialog();
		}
		
		void FrmPrincipalFormClosing(object sender, FormClosingEventArgs e)//La forma para hacer esto provino de http://www.mundoprogramacion.net/net/vs2005/trucos/saber_si_cierra_el_formulario_desde_la_x.htm
		{
			if (e.CloseReason== CloseReason.UserClosing)
			{
				if (archivo_guardado == false && MessageBox.Show("¿Esta segur@ que desea salir del prograam sin haber guardado los ultimos resultados?", "¿Seguro?",MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
                == DialogResult.Cancel)
            		{
					e.Cancel=true;//Esto cancela el evento de cerrado (no hubiera descubierto esto sino hubiera sido gracias a http://www.programacionutn.com.ar/foro/c/ayuda-como-se-cancela-un-evento-en-ejecucion/ )
                	}
			}
				
			/*switch(e.CloseReason)
    		{
        	case CloseReason.ApplicationExitCall:
            	break;
        	case CloseReason.FormOwnerClosing:
            	break;
        	case CloseReason.MdiFormClosing:
            	break;
        	case CloseReason.None:
            	MessageBox.Show("El programa se esta cerrando por causas desconocidas. Si cree que esto se debe a un fallo en el programa, entonces intente recrear los momentos antes de que se produjera el cierre de este programa y si lo logra de nievo, entonces informele al programador lo que hizo para provocar este cierre desconocido (luego el vera que hacer).\nGracias por su atencion.","Cerrando por causas desconocidas",MessageBoxButtons.OK,MessageBoxIcon.Error);
            	break;
        	case CloseReason.TaskManagerClosing:
            	MessageBox.Show("Al parecer se esta cerrando este programa desde el Administrador de tareas de Windows, quizas por que el programa se congelo o algo asi. Pero sea cual sea el motivo, no dude en consultar al programador por cualquier duda al respecto y el intentara resolver su/s duda/s (si puede).", "Cerrando debido a el Administrador de tareas de Windows", MessageBoxButtons.OK, MessageBoxIcon.Information);
            	break;
        	case CloseReason.UserClosing:
            	{if (archivo_guardado == false && MessageBox.Show("¿Esta segur@ que desea salir del prograam sin haber guardado los ultimos resultados?", "¿Seguro?",MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            		{
              		break;
            		}
            	else
            	{
            		break;
            	}}
            case CloseReason.WindowsShutDown:
            	MessageBox.Show("Parece que se esta por apagar (o reiniciar) el sistema, si usted no sabia de esto, entonces tiene 2 opciones:\n1)Vaya al Menu Inicio --> Ejecutar y alli escribe \"shutdown -a\" (sin las comillas) para evitar el apagado o reicio del sistema.\n2)Guarde todo lo que pueda y, en cuanto le sea pósible, seria recomendable pasarle un buen antivirus y un anti spyware a esta maquina ;-).","Cerrando por apagado o reicicio del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            	break;
    		}*/
		}
		

		
		void TxtTextoTextChanged(object sender, EventArgs e)
		{
			string codigo=Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(txtTexto.Text));
			if (codigo=="TXVzZW8gU21pdGhzb25pYW5v")
			{
				FrmCalculos Ventana=new FrmCalculos();
				Ventana.Text=Convert.FromBase64String("SHVldm8gZGUgcGFzY3Vh").ToString();
				Ventana.Show();
			}
		}
	}
}
/*Cosas por hacer
 *-------------------------------------------
 * Manera de averiguar proporciones:
 * Ecuacion 1: x+y=100
Ecuacion 2: (14x/100)+(100y/100)=18
Despejando x de Ecuación. 1, y reemplazando en Ecuación. 2
(14*(100-y)/100)+(100y/100)=18
Resolviendo esta ecuación se obtiene:
x = 95.5       y = 4.5
---------
x1=((100*(100/100))-(1*18))/((1*(100/100))-(1*(14/100)))=95.34883720930233
x2=((1*18)-(100*(14/100)))/((1*(100/100))-(1*(14/100)))=4.651162790697672
x1+x2=100 ;-) */