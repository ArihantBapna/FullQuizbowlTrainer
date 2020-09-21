package main;

import com.opencsv.bean.ColumnPositionMappingStrategy;
import com.opencsv.exceptions.CsvRequiredFieldEmptyException;

public class CustomMappingStrategy<T> extends ColumnPositionMappingStrategy<T> {
    private static final String[] HEADER = new String[]{"Id", "Question", "Answer", "Tournament", "Category", "Subcategory", "Prompt", "Alternate"};


    @Override
    public String[] generateHeader(T bean) throws CsvRequiredFieldEmptyException {
        return HEADER;
    }
}