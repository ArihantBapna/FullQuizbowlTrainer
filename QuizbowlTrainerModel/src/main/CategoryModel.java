package main;

import com.opencsv.bean.CsvBindByName;

public class CategoryModel {

    @CsvBindByName(column = "Category")
    private int category;
    @CsvBindByName(column = "Difficulty")
    private int difficulty;
    @CsvBindByName(column = "Points")
    private double points;
    @CsvBindByName(column = "Size")
    private double size;

    public double getSize() {
        return size;
    }

    public void setSize(double size) {
        this.size = size;
    }

    public int getCategory() {
        return category;
    }

    public void setCategory(int category) {
        this.category = category;
    }

    public int getDifficulty() {
        return difficulty;
    }

    public void setDifficulty(int difficulty) {
        this.difficulty = difficulty;
    }

    public double getPoints() {
        return points;
    }

    public void setPoints(double points) {
        this.points = points;
    }
}
