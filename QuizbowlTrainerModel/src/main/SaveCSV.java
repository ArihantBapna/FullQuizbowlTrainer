package main;

import com.opencsv.bean.StatefulBeanToCsv;
import com.opencsv.bean.StatefulBeanToCsvBuilder;
import com.opencsv.exceptions.CsvDataTypeMismatchException;
import com.opencsv.exceptions.CsvRequiredFieldEmptyException;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

public class SaveCSV {
    public static void SaveFinalQuestions(List<Data> qb){

        String fileName = "src/main/FinalQuestions.csv";
        Path myPath = Paths.get(fileName);
        try (var writer = Files.newBufferedWriter(myPath, StandardCharsets.UTF_8)) {
            CustomMappingStrategy<Data> mappingStrategy = new CustomMappingStrategy<>();
            mappingStrategy.setType(Data.class);
            StatefulBeanToCsv<Data> beanToCsv = new StatefulBeanToCsvBuilder<Data>(writer)
                    .build();

            beanToCsv.write(qb);
            writer.close();

        } catch (CsvDataTypeMismatchException | CsvRequiredFieldEmptyException |
                IOException ex) {
            Logger.getLogger(MainClass.class.getName()).log(
                    Level.SEVERE, ex.getMessage(), ex);
        }
    }

    public static void SaveClues(List<ClueModel> cm){
        String fileName = "src/main/CommonClues.csv";
        Path myPath = Paths.get(fileName);
        try (var writer = Files.newBufferedWriter(myPath, StandardCharsets.UTF_8)) {
            CustomMappingStrategy<ClueModel> mappingStrategy = new CustomMappingStrategy<>();
            mappingStrategy.setType(ClueModel.class);
            StatefulBeanToCsv<ClueModel> beanToCsv = new StatefulBeanToCsvBuilder<ClueModel>(writer)
                    .build();

            beanToCsv.write(cm);
            writer.close();

        } catch (CsvDataTypeMismatchException | CsvRequiredFieldEmptyException |
                IOException ex) {
            Logger.getLogger(MainClass.class.getName()).log(
                    Level.SEVERE, ex.getMessage(), ex);
        }
    }

    public static void SaveFinalAnswers(List<AnswerData> qb){

        String fileName = "src/main/FinalAnswers.csv";
        Path myPath = Paths.get(fileName);
        try (var writer = Files.newBufferedWriter(myPath, StandardCharsets.UTF_8)) {
            CustomMappingStrategy<AnswerData> mappingStrategy = new CustomMappingStrategy<>();
            mappingStrategy.setType(AnswerData.class);
            StatefulBeanToCsv<AnswerData> beanToCsv = new StatefulBeanToCsvBuilder<AnswerData>(writer)
                    .build();

            beanToCsv.write(qb);
            writer.close();

        } catch (CsvDataTypeMismatchException | CsvRequiredFieldEmptyException |
                IOException ex) {
            Logger.getLogger(MainClass.class.getName()).log(
                    Level.SEVERE, ex.getMessage(), ex);
        }
    }

    public static void SaveCategoricalModel(List<CategoryModel> cm){
        String fileName = "src/main/CategoryModel.csv";
        Path myPath = Paths.get(fileName);
        try (var writer = Files.newBufferedWriter(myPath, StandardCharsets.UTF_8)) {
            CustomMappingStrategy<CategoryModel> mappingStrategy = new CustomMappingStrategy<>();
            mappingStrategy.setType(CategoryModel.class);
            StatefulBeanToCsv<CategoryModel> beanToCsv = new StatefulBeanToCsvBuilder<CategoryModel>(writer)
                    .build();

            beanToCsv.write(cm);
            writer.close();

        } catch (CsvDataTypeMismatchException | CsvRequiredFieldEmptyException |
                IOException ex) {
            Logger.getLogger(MainClass.class.getName()).log(
                    Level.SEVERE, ex.getMessage(), ex);
        }
    }
}
