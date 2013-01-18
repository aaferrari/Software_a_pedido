/*
 * Creado por SharpDevelop.
 * Usuario: Usuario
 * Fecha: 04/03/2010
 * Hora: 03:17 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace Formulacion_del_nectar
{
	/// <summary>
	/// Description of Componentes.
	/// </summary>
	public class Componentes
	{
		//Los tipos de datos podrian haber sido decimales o double, pero opte por float por que es el tipo de datos que ocuma menos bytes en memoria (y tiene hasta 7 decimales despues de la coma, y no creo que hagan falta mas)
		float agua;
		float isomalt;
		float suclarosa;
		float acido_citrico;
		float sorbato;
		float pectina;
		float acido_ascórbico;
		float pulpa;
		float nectar;
			
		public float Agua
		{ 
		set {agua=value;}
		get	{return agua;}
		}
		
		public float Isomalt
		{ 
		set {isomalt=value;}
		get	{return isomalt;}
		}
		
		public float Suclarosa
		{ 
		set {suclarosa=value;}
		get	{return suclarosa;}
		}
		
		public float AcidoCitrico
		{ 
		set {acido_citrico=value;}
		get	{return acido_citrico;}
		}
		
		public float Sorbato
		{ 
		set {sorbato=value;}
		get	{return sorbato;}
		}
		
		public float Pectina
		{ 
		set {pectina=value;}
		get	{return pectina;}
		}
		
		public float Pulpa
		{ 
		set {pulpa=value;}
		get	{return pulpa;}
		}
		
		public float AcidoAscorbico
		{ 
		set {acido_ascórbico=value;}
		get	{return acido_ascórbico;}
		}
		
		public float Nectar
		{ 
		set {nectar=value;}
		get	{return nectar;}
		}
		
		public string TresSimple(float entrada)//Metodo para averiguar la cantidad de un determinado componente en la mezcla (esto hace todas las conversiones necesarias y evita agregarle codogo de mas al programa)
		{
			float resultado=(nectar*entrada)/100;
			if (resultado < 1)//Este if determina si el resultado esta en kilos o gramos 
			{
				return Convert.ToString(resultado*1000)+" g";
			}
			else
			{
				return Convert.ToString(resultado)+" Kg";
			}
		}
		
		/*public Componentes()
		{
			acido_ascórbico=0;
			acido_citrico=0;
			agua=0;
			isomalt=0;
			sorbato=0;
			suclarosa=0;
			pectina=0;
			pulpa=0;
			nectar=0;
		}
		*/
	}
}
