import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
public class resoconto {

    /**
     * @param args
     * @throws IOException
     */
    public static void main(String[] args) throws IOException {
        String nome1="";
        String nome2="";
        String nomesquadra="";
        Integer punteggio1=0;
        Integer punteggio2=0;
        Integer punteggiotot=0;
        Integer danni=0;
        File file = new File("classifica.csv");
        FileWriter fw = new FileWriter(file,true);
        fw.write("punti di " + nome1 + punteggio1 + "punti di " + nome2 + punteggio2 + " punti dela squadra " + nomesquadra + punteggiotot 
                    +" che ha ricevuro un tot di danni pari a " + danni);
        fw.flush();      
        fw.close();     




    }


}

