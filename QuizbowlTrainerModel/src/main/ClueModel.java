package main;

import com.opencsv.bean.CsvBindByName;

public class ClueModel {
    @CsvBindByName(column = "Clue")
    public String clue;
    @CsvBindByName(column = "Count")
    public int count;
    @CsvBindByName(column = "Difficulty")
    public int difficulty;
    @CsvBindByName(column = "Category")
    public int category;
    /*
    @CsvBindByName(column = "AnswerId")
    public int answerId;

    public int getAnswerId() {
        return answerId;
    }

    public void setAnswerId(int answerId) {
        this.answerId = answerId;
    }
*/
    public String getClue() {
        return clue;
    }

    public void setClue(String clue) {
        this.clue = clue;
    }

    public int getCount() {
        return count;
    }

    public void setCount(int count) {
        this.count = count;
    }

    public int getDifficulty() {
        return difficulty;
    }

    public void setDifficulty(int difficulty) {
        this.difficulty = difficulty;
    }

    public int getCategory() {
        return category;
    }

    public void setCategory(int category) {
        this.category = category;
    }

}
