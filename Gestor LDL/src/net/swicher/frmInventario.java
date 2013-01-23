package net.swicher;

import java.io.File;
import java.util.ArrayList;

import net.classes.ActividadDB;
import net.classes.GenericAdapter;
import net.classes.tuple;

import android.app.Activity;
import android.content.ContentValues;
import android.content.Intent;
import android.content.res.Resources;
import android.content.res.ColorStateList;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Environment;
import android.view.View;
import android.widget.Adapter;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.Spinner;
import android.widget.TabHost;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ToggleButton;

public class frmInventario extends ActividadDB {
	EditText txtPrecio;
	Spinner cmbEstantes;
	RadioButton SI;
	RadioButton NO;
	Spinner spnNombre;
	EditText txtNombre;
	Button btnGuardar;
	Button btnNuevo;
	TextView txvConfirmacion;
	tuple elegido;
	EditText codigoBarras;
	EditText txtModificado;
	EditText txtPais;
	Button btnModificar;
	Button btnCatalogo;
	int ultimo_articulo_visto;
	ToggleButton tglCamara;

    /** Called when the activity is first created. */
	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.abm);
        txtModificado = (EditText)findViewById(R.id.txtModificado);
        txtPais = (EditText)findViewById(R.id.txtPais);
        activador(false);
        rellenaSpinner(R.id.spnNombre, "select identificador, nombre from articulos order by nombre");
        rellenaSpinner(R.id.cmbEstantes, "select id_estante, estante from estantes order by id_estante");
        spnNombre.setOnItemSelectedListener(
            	new AdapterView.OnItemSelectedListener() {
            		public void onItemSelected(AdapterView<?> parent,
            			android.view.View v, int position, long id) {
            			elegido = (tuple)parent.getItemAtPosition(position);
            			getArticulo("identificador=?", new String[] {((Integer)elegido.identificador).toString()});
            		}

            		public void onNothingSelected(AdapterView<?> parent) {activador(false);}
            });
        
        btnNuevo = (Button)findViewById(R.id.btnNuevo);
        btnNuevo.setOnClickListener(new View.OnClickListener() {
        		@Override
        		public void onClick(View arg0)
        		{//Ponemos a la variable elegido con un objeto tuple vacio, llamamos al metodo activador y limpiamos los EditTexts
        			elegido = new tuple(-1, "");
        			activador(true);
        			txtNombre.setText("");
        			txtModificado.setText("");
        			txtPais.setText("");
        			txtPrecio.setText("");
        			codigoBarras.setText("");
        			SI.setChecked(false);
        			NO.setChecked(false);
        			txvConfirmacion.setText("");
        			cmbEstantes.setSelection(0);
        		}
        	});
        txvConfirmacion = (TextView)findViewById(R.id.txvConfirmacion);
        btnGuardar.setOnClickListener(new View.OnClickListener() {
        	@Override
    		public void onClick(View arg0)
    		{
    			ContentValues registro = new ContentValues();
				String codigo = codigoBarras.getText().toString();
				registro.put("codigo_barra", codigo);
				registro.put("nombre", txtNombre.getText().toString());
				registro.put("precio", txtPrecio.getText().toString());
				registro.put("estante", ((tuple)cmbEstantes.getSelectedItem()).identificador);
				if (codigo.length() > 3) registro.put("pais", codigo.substring(0,3));
				if (SI.isChecked() == true) registro.put("en_venta", 1);
				else registro.put("en_venta", 0);
				try {
					if (elegido.identificador == -1){
						base_datos.insert("articulos", null, registro);
						rellenaSpinner(R.id.spnNombre, "select identificador, nombre from articulos order by nombre");
						Adapter listado = spnNombre.getAdapter();
						//Buscar una mejor forma de iterar sobre un adaptador
						for (int contador=0; contador < listado.getCount(); contador++){
							tuple articulo = (tuple) listado.getItem(contador);
							if (articulo.texto.equals(registro.get("nombre"))){
								spnNombre.setSelection(contador);
								activador(false);
								break;
							}
						}
					}
					else {
						base_datos.update("articulos", registro, "identificador=" + elegido.identificador, null);
						activador(false);
						getArticulo("identificador=?", new String[] {((Integer)elegido.identificador).toString()});
					}
					txvConfirmacion.setTextColor(Color.GREEN);//verde
					txvConfirmacion.setText("El articulo se ha guardado exitosamente");
				}
				catch(SQLException excepcion) {
					txvConfirmacion.setTextColor(Color.RED);//rojo
					txvConfirmacion.setText("Hubo problemas para guardar los datos del articulo, por favor intente de nuevo (excepcion " + excepcion.getMessage() + ")");
				}
				txvConfirmacion.append("\nEstado de unidad externa: " + Environment.getExternalStorageState());
    		}
        });
        
        btnModificar = (Button)findViewById(R.id.btnModificar);
        btnModificar.setOnClickListener(new View.OnClickListener() {
        	@Override
    		public void onClick(View arg0)
    		{activador(true);}
        });
        btnCatalogo = (Button)findViewById(R.id.btnCatalogo);
        btnCatalogo.setOnClickListener(new View.OnClickListener() {
        	@Override
    		public void onClick(View arg0)
    		{
        		activador(false);
        		spnNombre.setSelection(ultimo_articulo_visto);
        	}
        });
        tglCamara = (ToggleButton)findViewById(R.id.tglCamara);
        tglCamara.setOnClickListener(new View.OnClickListener() {
        	@Override
        	public void onClick(View arg0)
        	{
        		// Para hacer andar esto se lo pudo resolver mediante un enlace de Google Groups que aparece en http://co.runcode.us/q/compilation-error-on-zxing
        		if (tglCamara.isChecked()){
        			//Inciamos la activity del ZXing y luego ponemos los resultados en un EditText
        			Intent escaneador = new Intent("com.google.zxing.client.android.SCAN");
        			escaneador.putExtra("SCAN_MODE", "ONE_D_FORMATS");
        			startActivityForResult(escaneador, 0);
        		}
        	}
        });
    }
    
    public void activador(boolean indicador){
    	txtPrecio = (EditText)findViewById(R.id.txtPrecio);
    	cmbEstantes = (Spinner)findViewById(R.id.cmbEstantes);
    	SI = (RadioButton)findViewById(R.id.radio0);
    	NO = (RadioButton)findViewById(R.id.radio1);
    	FrameLayout frlArticulo = (FrameLayout)findViewById(R.id.frlArticulo);
    	spnNombre = (Spinner)findViewById(R.id.spnNombre);
    	txtNombre = (EditText)findViewById(R.id.txtNombre);
    	FrameLayout frlDesiciones = (FrameLayout)findViewById(R.id.frlDecisiones);
    	FrameLayout frlAcciones = (FrameLayout)findViewById(R.id.frlAcciones);
    	btnGuardar = (Button)findViewById(R.id.btnGuardar);
    	btnNuevo = (Button)findViewById(R.id.btnNuevo);
    	btnModificar = (Button)findViewById(R.id.btnModificar);
    	btnCatalogo = (Button)findViewById(R.id.btnCatalogo);
    	
    	txtPrecio.setEnabled(indicador);
    	cmbEstantes.setEnabled(indicador);
    	SI.setEnabled(indicador);
    	NO.setEnabled(indicador);
    	// http://www.javaya.com.ar/androidya/detalleconcepto.php?codigo=149&inicio=0
    	if (indicador == true) {
    		//La modificacion de la siguiente variable sirve para que despues se pueda volver al ultimo articulo seleccionado cuando se cancela la operacion de modificacion o creacion de articulos
    		ultimo_articulo_visto = spnNombre.getSelectedItemPosition();
    		spnNombre.setVisibility(frlArticulo.INVISIBLE);
    		txtNombre.setVisibility(frlArticulo.VISIBLE);
    		btnGuardar.setVisibility(frlDesiciones.VISIBLE);
    		btnNuevo.setVisibility(frlDesiciones.INVISIBLE);
    		btnModificar.setVisibility(frlAcciones.INVISIBLE);
    		btnCatalogo.setVisibility(frlAcciones.VISIBLE);
    	} else {
    		spnNombre.setVisibility(frlArticulo.VISIBLE);
    		txtNombre.setVisibility(frlArticulo.INVISIBLE);
    		btnGuardar.setVisibility(frlDesiciones.INVISIBLE);
    		btnNuevo.setVisibility(frlDesiciones.VISIBLE);
    		btnModificar.setVisibility(frlAcciones.VISIBLE);
    		btnCatalogo.setVisibility(frlAcciones.INVISIBLE);
    	}
    }
    
    public void onActivityResult(int requestCode, int resultCode, Intent intent) {
    	   if (requestCode == 0) {
    	      if (resultCode == RESULT_OK) {
    	         String codigo = intent.getStringExtra("SCAN_RESULT");
    	         codigoBarras.setText(codigo);
    	         //String formato = intent.getStringExtra("SCAN_RESULT_FORMAT");
    	         getArticulo("codigo_barra=?", new String[] {codigoBarras.getText().toString()});
    	      } else if (resultCode == RESULT_CANCELED) {
    	    	  txvConfirmacion.setTextColor(Color.RED);//rojo
				  txvConfirmacion.setText("El lector fue apagado abruptamente");
    	      }
    	      tglCamara.setChecked(false);
    	   }
    	}
    
    public void getArticulo(String filtro, String[] condicionales){
    	//Hace un select a la base de datos en base al campo indicado como condicion y asigna la informacion del primer registro (si existe) en los controles correspondientes
    	String[] campos = {"nombre", "precio", "estante", "strftime('%d/%m/%Y a las %H:%M:%S', datetime(fecha_actualizacion, 'localtime')) actualizado", "(select pais from prefijosEAN where id = articulos.pais) as pais", "codigo_barra", "en_venta"};
		Cursor puntero = base_datos.query("articulos", campos, filtro, condicionales, null, null, null);
		if (puntero.moveToFirst()) {
			txtNombre.setText(puntero.getString(0));
			txtPrecio.setText(((Float)puntero.getFloat(1)).toString());
			int vendible = puntero.getInt(6);
			if (vendible == 1) SI.setChecked(true);
			else NO.setChecked(true);
			txtModificado.setText(puntero.getString(3));
			txtPais.setText(puntero.getString(4));
			codigoBarras = (EditText)findViewById(R.id.txtCodigo);
			codigoBarras.setText(puntero.getString(5));
			cmbEstantes.setSelection(puntero.getInt(2));
		}
		puntero.close();
    }
    
    public void rellenaSpinner(int idSpinner, String sentencia){
        //Metodo para rellenar genericamente Spinners con objetos "tuple" mediante una sentencia select que devuelva datos compatibles
    	Spinner spinner = (Spinner)findViewById(idSpinner);
        Cursor consulta = base_datos.rawQuery(sentencia, null);
        ArrayList<Object> conjuntos1 = new ArrayList<Object>();
        //Nos aseguramos de que existe al menos un registro
        if (consulta.moveToFirst()) {
             //Recorremos el cursor hasta que no haya más registros
             do {
            	 conjuntos1.add(new tuple(consulta.getInt(0), consulta.getString(1)));
             } while(consulta.moveToNext());
        }
        consulta.close();
        GenericAdapter adaptador = new GenericAdapter(conjuntos1, "texto", getApplicationContext());
        spinner.setAdapter(adaptador);
        spinner.setSelection(-1);
        }

    @Override    
    protected void onDestroy() {
    	//Este metodo se debe colocar en todos los hijos de la clase principal para evitar el error descrito en http://stackoverflow.com/questions/1556930/sharing-sqlite-database-between-multiple-android-activities
        super.onDestroy();
        base_datos.close();
    }

}