package main;

import org.apache.commons.math3.stat.descriptive.DescriptiveStatistics;

import java.util.*;
import java.util.function.Function;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

public class AnswerAnalysis {

    public static void WeighAnswers(List<AnswerModel> Ans) {

        //Filter by difficulty first
        System.out.println("Filtering by difficulty starts");

        List<Integer> diffs = new ArrayList<>();
        int[] cats = new int[] {14,15,16,17,18,19,20,21,22,25,26};

        List<List<AnswerModel>> filteredByDiff = new ArrayList<>();

        for(int i=1;i<=9;i++){
            List<AnswerModel> filteredDiff = new ArrayList<>();
            for(AnswerModel ans : Ans){
                AnswerModel filtAns = new AnswerModel();
                List<QuestionModel> filtQ = GetSelectedDiff(ans.getQuestions(),i);

                if(filtQ.size() > 0){
                    filtAns.setAnswer(ans.getAnswer());
                    filtAns.setQuestions(filtQ);
                    filtAns.setSize(filtQ.size());

                    filteredDiff.add(filtAns);
                }
            }
            filteredByDiff.add(filteredDiff);
        }
        System.out.println("Done filtering my difficulty");
        List<List<List<AnswerModel>>> Database = new ArrayList<>();
        System.out.println("Filtering by category");
        for(int i=0;i<9;i++){
            List<AnswerModel> diffList = filteredByDiff.get(i);
            List<List<AnswerModel>> filteredCat = new ArrayList<>();
            for(int c : cats){
                List<AnswerModel> firstStepCat = new ArrayList<>();
                for(AnswerModel ans : diffList){
                    AnswerModel filteredAnswers = new AnswerModel();
                    List<QuestionModel> q = GetSelectedCategory(ans.getQuestions(),c);
                    if(q.size() > 0){
                        filteredAnswers.setAnswer(ans.getAnswer());
                        filteredAnswers.setQuestions(q);
                        filteredAnswers.setSize(q.size());
                        firstStepCat.add(filteredAnswers);
                    }
                }
                filteredCat.add(firstStepCat);
            }
            Database.add(filteredCat);
        }
        System.out.println("Done filtering by category");
        System.out.println("Outputting db");
        List<List<List<AnswerModel>>> CleanedDatabase = RemoveEmpties(Database);
        SortList(CleanedDatabase);
        System.out.println("Making final lists");
        CreateMainList(CleanedDatabase);
        System.out.println("Creating categorical model");
        CreateCategoricalModel(CleanedDatabase);
        System.out.println("Clue analysis");
        MostCommon(CleanedDatabase);
        //CheckList(CleanedDatabase);

    }

    public static void SortList(List<List<List<AnswerModel>>> db){
        for(List<List<AnswerModel>> byDiffs : db){
            for(List<AnswerModel> byCats : byDiffs){
                byCats.sort(Comparator.comparingDouble(AnswerModel::getSize).reversed());
            }
        }
    }

    public static void MostCommon(List<List<List<AnswerModel>>> db){
        List<ClueModel> cm = new ArrayList<>();
        List<ClueModel> Clues = new ArrayList<>();
        for(List<List<AnswerModel>> byDiffs : db){
            for(List<AnswerModel> byCats : byDiffs){
                List<AnswerModel> Top5 = new ArrayList<>();
                for(int i=0;i<5;i++){
                    Top5.add(byCats.get(i));
                }
                for(AnswerModel ans : Top5){
                    int diff = ans.getQuestions().get(0).getDiff();
                    int category = ans.getQuestions().get(0).getCategory();
                    Clues.addAll(GetCluesFromQuestions(ans.getQuestions()));
                }
            }
        }
        Iterator<ClueModel> ClueIt = Clues.iterator();
        while(ClueIt.hasNext()){
            ClueModel c = ClueIt.next();
            String[] words = c.getClue().split(" ");
            if(words.length < 5){
                ClueIt.remove();
            }
        }
        List<String> AllClues = new ArrayList<>();
        for(ClueModel c1 : Clues){
            AllClues.add(c1.getClue());
        }
        Map<String, Long> counter = AllClues.stream().collect(Collectors.groupingBy(Function.identity(), Collectors.counting()));
        for(Map.Entry<String, Long> entry : counter.entrySet()){
            ClueModel c = new ClueModel();
            c.setClue(entry.getKey());
            c.setCount(Math.toIntExact(entry.getValue()));
            for(ClueModel c1 : Clues){
                if(c1.getClue().equals(entry.getKey())){
                    c.setDifficulty(c1.getDifficulty());
                    c.setCategory(c1.getCategory());
                    //c.setAnswerId(c1.getAnswerId());
                }
            }
            cm.add(c);
        }
        //Save csv
        SaveCSV.SaveClues(cm);
    }

    public static List<ClueModel> GetCluesFromQuestions(List<QuestionModel> q1){
        List<ClueModel> ClueList = new ArrayList<>();
        for(QuestionModel q : q1){
            String question = q.getQuestion();
            Pattern re = Pattern.compile("[^.!?\\s][^.!?]*(?:[.!?](?!['\"]?\\s|$)[^.!?]*)*[.!?]?['\"]?(?=\\s|$)", Pattern.MULTILINE | Pattern.COMMENTS);
            Matcher reMatcher = re.matcher(question);
            while(reMatcher.find()){
                ClueModel c = new ClueModel();
                c.setClue(reMatcher.group().toLowerCase());
                c.setCategory(q.getCategory());
                c.setDifficulty(q.getDiff());
                //c.setAnswerId(q.getAnswerId());
                ClueList.add(c);
            }
        }
        return ClueList;
    }

    public static double CalculatePoints(int diff, int size){
        double k = 10 * diff;
        double numerator = k * Math.pow(Math.PI,2);
        double inLog = Math.pow(k,2) + 1;
        double denominator = 6 * Math.log10(inLog);
        double points = numerator / denominator;
        double totalPoints = points * size;

        return totalPoints;
    }

    public static void CreateCategoricalModel(List<List<List<AnswerModel>>> db){
        int diff = 0;
        List<CategoryModel> RubricModel = new ArrayList<>();
        for(List<List<AnswerModel>> byDiffs : db){
            diff++;
            for(List<AnswerModel> byCats : byDiffs){
                CategoryModel catMod = new CategoryModel();
                int sizes = 0;
                for(AnswerModel ans : byCats){
                    sizes+=1;
                    catMod.setCategory(ans.getQuestions().get(0).getCategory());
                }
                catMod.setPoints(CalculatePoints(diff,sizes));
                catMod.setDifficulty(diff);
                catMod.setSize(sizes);
                RubricModel.add(catMod);
            }
        }
        SaveCSV.SaveCategoricalModel(RubricModel);
    }

    public static void CreateMainList(List<List<List<AnswerModel>>> db){
        List<Data> FinalQuestions = new ArrayList<>();
        List<AnswerData> FinalAnswers = new ArrayList<>();
        int finCount = 0;
        for(List<List<AnswerModel>> byDiffs : db){
            for(List<AnswerModel> byCats : byDiffs){

                int count = 0;
                for(AnswerModel ans : byCats){
                    count+=ans.getSize();
                }
                int count2 = 0;
                int i =0;
                while((double) count2 <= 0.6 * (double) count){
                    AnswerModel a = byCats.get(i);

                    AnswerData aD = new AnswerData();
                    aD.setAnswer(a.getAnswer());
                    aD.setSize(a.getSize());
                    aD.setId(finCount);

                    FinalAnswers.add(aD);

                    List<QuestionModel> q1 = a.getQuestions();
                    for(QuestionModel q : q1){
                        Data d = new Data();
                        aD.setDiff(q.getDiff());
                        aD.setCategory(q.getCategory());
                        for(Data d1 : MainClass.FinalFiltered){
                            if(d1.getId() == q.getId()){
                                d = d1;
                                d.setAnsId(aD.getId());
                            }
                        }
                        FinalQuestions.add(d);
                    }

                    double score = (10*aD.getDiff()*(aD.getSize() - (aD.getCorr()*Math.log10(10*Math.pow(aD.getDiff(),2)+1)))) + 10*aD.getDiff();
                    aD.setScore(score);
                    double rating = ((200*aD.getDiff()) - (5 * aD.getSize()));
                    aD.setRating(rating);
                    i++;
                    count2+=a.getSize();
                    finCount++;
                }
            }
        }

        System.out.println("Total number of questions saved: " +FinalQuestions.size());
        SaveCSV.SaveFinalQuestions(FinalQuestions);
        System.out.println("Total number of answer lines: " +FinalAnswers.size());
        SaveCSV.SaveFinalAnswers(FinalAnswers);
    }

    public static void CheckList(List<List<List<AnswerModel>>> db){
        int diffCount = 0;
        int[] cats = new int[] {14,15,16,17,18,19,20,21,22,25,26};
        int catCounter = 0;
        int total = 0;
        for(List<List<AnswerModel>> byDiffs : db){
            System.out.println("On difficulty:" +diffCount);
            catCounter = 0;
            for(List<AnswerModel> byCats : byDiffs){
                DescriptiveStatistics stats = new DescriptiveStatistics();
                int c = cats[catCounter];

                int withQ = 0;
                System.out.println("\t Category: " +c);

                for(AnswerModel ans : byCats){
                    stats.addValue(ans.getSize());
                    if(ans.getQuestions().size() > 0){
                        withQ+= ans.getQuestions().size();
                    }
                    System.out.println("\t \t" +ans.getAnswer() +": " +ans.getSize());
                    //System.out.println("Answer - " +ans.getAnswer() +" - Count - " +ans.getSize());
                }
                //System.out.println("Category: " +c +" Mean: " +stats.getMean() +" Std: " +stats.getStandardDeviation() +" Answer lines: " +byCats.size() +" Questions: " +withQ);

                total += withQ;
                catCounter++;
            }
            diffCount++;
        }
        System.out.println("Indexed: " +total +" questions");
    }

    public static List<List<List<AnswerModel>>> RemoveEmpties(List<List<List<AnswerModel>>> db){

        for(List<List<AnswerModel>> byDiffs : db){
            for(List<AnswerModel> byCats : byDiffs){
                int count =0;
                List<Integer> removeThese = new ArrayList<>();
                for(AnswerModel ans : byCats){
                    if(ans.getAnswer().equals("") || ans.getAnswer().equals(" ")){
                        removeThese.add(count);
                    }
                    count++;
                }
                for(int i : removeThese){
                    byCats.remove(i);
                }
            }
        }
        return db;
    }

    public static List<QuestionModel> SetAnswerId(List<QuestionModel> q1){
        for(QuestionModel q : q1){
            int id = q.getId();
            for(Data d : MainClass.FinalFiltered){
                if(d.getId()==id){
                    q.setAnswerId(d.getAnsId());
                }
            }

        }
        return q1;
    }

    public static List<QuestionModel> GetSelectedCategory(List<QuestionModel> q1, int cat){
        List<QuestionModel> filteredQ = new ArrayList<>();
        for(QuestionModel q : q1){
            if(q.getCategory() == cat){
                filteredQ.add(q);
            }
        }
        return filteredQ;
    }

    public static List<QuestionModel> GetSelectedDiff(List<QuestionModel> q1, int diff){
        List<QuestionModel> filteredQ = new ArrayList<>();
        for(QuestionModel q : q1){
            if(q.getDiff() == diff){
                filteredQ.add(q);
            }
        }
        return filteredQ;
    }

}
