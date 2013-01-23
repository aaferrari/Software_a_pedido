package net.classes;

import java.lang.reflect.Field;
import java.util.ArrayList;

import android.content.ContentValues;
import android.content.Context;
import android.database.DataSetObserver;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.SpinnerAdapter;
import android.widget.TextView;

public class GenericAdapter implements SpinnerAdapter{
    /**
     * The internal data (the ArrayList with the Objects).
     */
    ArrayList<Object> data;
    Context context;
    String visible_text;
    
    public GenericAdapter(ArrayList<Object> data, String visible_text, Context context){
        this.data = data;
        this.context = context;
        this.visible_text = visible_text;
    }

	@Override
	public int getCount() {
		return data.size();
	}

	@Override
	public Object getItem(int position) {
		return data.get(position);

	}

	@Override
	public long getItemId(int position) {
		return position;
	}

	@Override
	public int getItemViewType(int arg0) {
		return android.R.layout.simple_spinner_dropdown_item;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		TextView v = new TextView( context.getApplicationContext());
        v.setTextColor(Color.BLACK);
        v.setText(getVisibleText(position));
        return v;

	}

	@Override
	public int getViewTypeCount() {
		return android.R.layout.simple_spinner_dropdown_item;
	}

	@Override
	public boolean hasStableIds() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public boolean isEmpty() {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	public void registerDataSetObserver(DataSetObserver arg0) {
		// TODO Auto-generated method stub
	}

	@Override
	public void unregisterDataSetObserver(DataSetObserver arg0) {
		// TODO Auto-generated method stub

	}

	@Override
	public View getDropDownView(int position, View convertView, ViewGroup parent) {
		if (convertView == null)
		  {
		    LayoutInflater vi = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
		    convertView = vi.inflate(android.R.layout.simple_spinner_dropdown_item, null);
		  }

		  TextView textView = (TextView) convertView.findViewById(android.R.id.text1);
		  textView.setText(getVisibleText(position));
		  return convertView;
	}
	
	public String getVisibleText(int position){
		/** http://www.arumeinformatica.es/blog/java-reflection-parte-2/
		 * Estrae el atributo de texto que se quiere mostrar en el item del Spinner mediante reflection
		 */
        Class<?> item = data.get(position).getClass();
        Field text = null;
        String output = null;
		try {
			text = item.getDeclaredField(visible_text);
		} catch (SecurityException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		} catch (NoSuchFieldException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
        try {
			output = (String)text.get(data.get(position));
		} catch (IllegalArgumentException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return output;
	}

}
