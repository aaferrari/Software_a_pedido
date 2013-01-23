package net.classes;

import java.io.File;

import android.app.Activity;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.os.Environment;
import android.widget.Toast;

public class ActividadDB extends Activity {
	public SQLiteDatabase base_datos;
	String ruta_tarjetaSD = Environment.getExternalStorageDirectory().getAbsolutePath();
    // Ya que en Java no existe un metodo para unir rutas de archivo (como el metodo join de os.path en Python) entonces se opto por usar un truco que se muestra en http://stackoverflow.com/questions/711993/does-java-have-a-path-joining-method
    final String rutaDB = new File(ruta_tarjetaSD, "Base de datos LDL.sqlite").toString();
    
    /** Called when the activity is first created. */
	@Override
    public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		base_datos = SQLiteDatabase.openOrCreateDatabase(rutaDB, null);
	}
	
	@Override
    protected void onPause() {
		if (base_datos.isOpen() && base_datos != null) {
		//Cerramos la base de datos para pasar a otra activity
		base_datos.close();
		//http://youropensource.com/projects/576-alert-or-MessageBox-in-android
		Toast.makeText(this, "La base de datos se ha cerrado al finalizar la actividad anterior", Toast.LENGTH_SHORT).show();
		}
		super.onPause();
	}
	
	@Override
    protected void onResume() {
		super.onResume();
		if (base_datos != null) {
		//Reabrimos la base de datos cuando se vuelve a esta actividad
        base_datos = SQLiteDatabase.openOrCreateDatabase(rutaDB, null);
        //http://youropensource.com/projects/576-alert-or-MessageBox-in-android
		Toast.makeText(this, "La base de datos se ha reconectado", Toast.LENGTH_SHORT).show();
		}
	}
}
