package net.swicher;
/** Fuentes usadas para el manejo de Tabhost:
 * http://android.cix.pe/2011/09/tabhost/
 * http://mobile.davidocs.com/android/disenando-la-aplicacion-uso-de-tabhost-en-android/
 */

import java.io.File;

import android.app.Activity;
import android.app.ActivityGroup;
import android.app.LocalActivityManager;
import android.app.TabActivity;
import android.content.Intent;
import android.content.res.Resources;
import android.content.*;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.os.Environment;
import android.preference.PreferenceManager;
import android.view.View;
import android.widget.TabHost;

public class GestorLDLActivity extends TabActivity {
    /** Called when the activity is first created. */
	private static final String ULTIMA_TAB = "Pestaña actual";
	private TabHost contenedor;
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        Resources recursos = getResources();
        contenedor = (TabHost)findViewById(android.R.id.tabhost);
        agregar_tabs("mitab1", "Inventario", frmInventario.class);
        agregar_tabs("mitab2", "Buscador", frmBuscador.class);
        // Al abrir la aplicacion restauramos la última pestaña activada
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(this);
        int currentTab = prefs.getInt(ULTIMA_TAB, 0);
        contenedor.setCurrentTab(currentTab);   
    }
    
    @Override
    protected void onPause() {
        super.onPause();

        // Cuando se cierra la aplicación guardamos la pestaña activa
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(this);
        SharedPreferences.Editor editor = prefs.edit();
        int currentTab = contenedor.getCurrentTab();
        editor.putInt(ULTIMA_TAB, currentTab);
        editor.commit();
    }
    
    public void agregar_tabs(String nombre, String titulo, Class<?> clase){
        TabHost.TabSpec spec= contenedor.newTabSpec(nombre);
        Intent ejecutor = new Intent(this, clase);// http://stackoverflow.com/questions/5575914/android-tab-layout-help
        //Bundle paquete = new Bundle();
        //paquete.pu("base_datos", base_datos);
        spec.setIndicator(titulo);
        spec.setContent(ejecutor);
        contenedor.addTab(spec);
    }

}