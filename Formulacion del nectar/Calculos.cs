/*
 * Creado por SharpDevelop.
 * Usuario: Usuario
 * Fecha: 14/03/2010
 * Hora: 03:34 a.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace Formulacion_del_nectar
{
	/// <summary>
	/// Description of FrmCalculos.
	/// </summary>
	public partial class FrmCalculos : Form
	{
		
		public FrmCalculos()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			inicializador();
		}
		
		public void inicializador()
		{
						System.Text.ASCIIEncoding Operador1 = new System.Text.ASCIIEncoding();
		//	System.Text.Decoder Operador2 = Operador1.GetDecoder();
			byte[] Resultado = Convert.FromBase64String("SHVldm8gZGUgcGFzY3Vh");
        	/*int Conteo = Operador1.GetCharCount(Resultado, 0, Resultado.Length);    
       		char[] Cadena = new char[Conteo];
        	Operador2.GetChars(Resultado, 0, Resultado.Length, Cadena, 0);
        	string texto=new String(Cadena);*/
			this.Text=Operador1.GetString(Resultado);
			Label lblTexto=new Label();
			lblTexto.Location=new Point(4,8);
			lblTexto.Size=new Size(119,284);
			Resultado=Convert.FromBase64String("SHVldm8gZGUgcGFzY3Vh"/*"UHJvZ3JhbWEgaGVjaG8gZXNwZWNpYWxtZW50ZSBwYXJhIG1pIGFtaWdhIENlY2lsaWEgZGUgQ29sb21iaWEgc2VndW4gc3VzIGVzcGVjaWZpY2FjaW9uZXMuDQpEZSBtb21lbnRvIGVzdG8gZXMgbG8gdW5pY28gcXVlIHB1ZWRvIG9jdWx0YXIgZGVudHJvIGRlbCBwcm9ncmFtYSBjb21vIHVuIGh1ZXZvIGRlIHBhc2N1YSAocXVpemFzIGFsZ3VuIGRpYSBwdWVkYSBtZXRlciBhbGd1biBqdWVnaXRvLCBhc2kgYWwgbWVub3MgZXN0byBzZXJpYSBtYXMgaW50ZXJlc2FudGUpLg0KUGFyYSBjb25zdWx0YXMgeSBzdWdlcmVuY2lhcywgbWkgZW1haWwgZXMgc3dpY2hlckBnbWFpbC5jb20gKGF2aXNvIHF1ZSBlc3RlIGVtYWlsIG5vIGZ1bmNpb25hIGNvbiBlbCBXaW5kb3dzIExpdmUgTWVzc2VuZ2VyKS4"*/);
			lblTexto.Text=Operador1.GetString(Convert.FromBase64String("SHVldm8gZGUgcGFzY3Vh"));

		}
	}
}
