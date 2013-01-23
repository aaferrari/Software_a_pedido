package net.classes;

public class tuple {
        /* http://stackoverflow.com/questions/6562236/android-spinner-databind-using-array-list
         * Normally you would create some getter and
         * setter-methods. But in Android, using
         * public fields is better for memory
         * proposals.
         */
	public int identificador;    
	public String texto;
   
	public tuple(int identificador, String texto){
       this.texto = texto;
        this.identificador = identificador;
        }
}
