package net.swicher;

import java.util.ArrayList;

import net.classes.ActividadDB;
import net.classes.tuple;
import android.app.Activity;
import android.database.Cursor;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.text.method.KeyListener;
import android.view.KeyEvent;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.GridView;
import android.widget.TextView;

public class frmBuscador extends ActividadDB {
	    /** Called when the activity is first created. */
	    @Override
	    public void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.buscador);
	        final TextView txtBusqueda = (EditText)findViewById(R.id.txtBusqueda);
	        final TextView txtMenor = (EditText)findViewById(R.id.txtPrecioMenor);
	        final TextView txtMayor = (EditText)findViewById(R.id.txtPrecioMayor);
	        txtBusqueda.addTextChangedListener(new TextWatcher() {
	            public void afterTextChanged(Editable s) {
	            	//Gracias a http://android.sentidoweb.com/2011/06/27/evento-onchange-en-edittext-en-android/ (cuyo ejemplo fue copupasteado de http://www.andreabaccega.com/blog/2010/10/09/onchange-event-on-edittext-in-android ) por este truco para ejecutar codigo despues de escribir texto
	            	buscador(Float.valueOf(txtMenor.getText().toString()), Float.valueOf(txtMayor.getText().toString()), txtBusqueda.getText().toString());
	             }
	             public void beforeTextChanged(CharSequence s, int start, int count,
	                    int after) {
	                // TODO Auto-generated method stub
	             }
	             public void onTextChanged(CharSequence s, int start, int before,
	                    int count) {
	                // TODO Auto-generated method stub
	            }
	         });

	    }
	    
	    @Override    
	    protected void onDestroy() {
	    	//Este metodo se debe colocar en todos los hijos de la clase principal para evitar el error descrito en http://stackoverflow.com/questions/1556930/sharing-sqlite-database-between-multiple-android-activities
	        super.onDestroy();
	        base_datos.close();
	    }
	    
	    public void buscador(float numero1, float numero2, String termino_busqueda){
	    	float precios[] = {numero1, numero2};
 	       String consulta_base = "select nombre, precio from articulos where nombre like ? and precio ";
 	       Cursor busqueda;
 	       if (precios[0] == precios[1])
 	    	   busqueda = base_datos.rawQuery(consulta_base + ">= ?", new String[] {'%'+termino_busqueda+'%', Float.toString(numero1)});
 	       else
 	    	   busqueda = base_datos.rawQuery(consulta_base + "between ? and ?", new String[] {'%'+termino_busqueda+'%', Float.toString(Math.min(precios[0], precios[1])), Float.toString(Math.max(precios[0], precios[1])) } );
 	       ArrayList<String> resultados = new ArrayList<String>();
 	       resultados.add("Nombre del articulo"); resultados.add("Precio");//Cabeceras de la tabla
 	        //Nos aseguramos de que existe al menos un registro
 	        if (busqueda.moveToFirst()) {
 	             //Recorremos el cursor hasta que no haya más registros
 	             do {
 	            	 resultados.add(busqueda.getString(0));
 	            	 resultados.add("$" + busqueda.getString(1));
 	             } while(busqueda.moveToNext());
 	        }
 	       busqueda.close();
 	       ArrayAdapter<String> adaptador = new ArrayAdapter<String>(getApplicationContext(), android.R.layout.simple_list_item_1, resultados);
 	       GridView grdResultados = (GridView)findViewById(R.id.gridView1);
 	       grdResultados.setAdapter(adaptador);
	    }
	}
