package contrato;

import java.io.Serializable;

public class Reply  implements Serializable {
    private String word;
    private int maxSize;

    public String getWord() {
        return word;
    }

    public void setWord(String word) {
        this.word = word;
    }

    public int getMaxSize() {
        return maxSize;
    }

    public void setMaxSize(int maxSize) {
        this.maxSize = maxSize;
    }
}
