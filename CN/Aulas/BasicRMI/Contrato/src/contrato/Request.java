package contrato;

import java.io.Serializable;
import java.util.List;

public class Request implements Serializable {
    private List<String> textLines;
    public List<String> getTextLines() {
        return textLines;
    }

    public void setTextLines(List<String> textLines) {

        this.textLines = textLines;
    }
}
