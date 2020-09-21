package main;

import com.opencsv.bean.CsvBindByName;

public class AnswerData {

    @CsvBindByName(column = "Answer")
    private String Answer;
    @CsvBindByName(column = "Size")
    private int Size;
    @CsvBindByName(column = "Id")
    private int Id;
    @CsvBindByName(column = "Corrects")
    private int corr=0;
    @CsvBindByName(column = "Negs")
    private int negs=0;
    @CsvBindByName(column = "Category")
    private int category;
    @CsvBindByName(column = "Difficulty")
    private int diff;
    @CsvBindByName(column = "Score")
    private double score;
    @CsvBindByName(column = "Rating")
    private double rating;

    public double getRating() {
        return rating;
    }

    public void setRating(double rating) {
        this.rating = rating;
    }

    public double getScore() {
        return score;
    }

    public void setScore(double score) {
        this.score = score;
    }

    public int getCategory() {
        return category;
    }

    public void setCategory(int category) {
        this.category = category;
    }

    public int getDiff() {
        return diff;
    }

    public void setDiff(int diff) {
        this.diff = diff;
    }

    public int getCorr() {
        return corr;
    }

    public void setCorr(int corr) {
        this.corr = corr;
    }

    public int getNegs() {
        return negs;
    }

    public void setNegs(int negs) {
        this.negs = negs;
    }



    public String getAnswer() {
        return Answer;
    }

    public void setAnswer(String answer) {
        Answer = answer;
    }

    public int getSize() {
        return Size;
    }

    public void setSize(int size) {
        Size = size;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }
}
