package main;

import com.opencsv.bean.*;

import java.io.BufferedReader;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.*;
import java.util.logging.Level;
import java.util.logging.Logger;

import com.opencsv.exceptions.CsvDataTypeMismatchException;
import com.opencsv.exceptions.CsvRequiredFieldEmptyException;

public class MainClass {

    public static List<Data> FinalFiltered = new ArrayList<>();

    public static void main(String[] args) throws IOException, ClassNotFoundException {

        List<DataRaw> qbr = GetData();
        List<Tournaments> tourney = GetTournamentData();
        System.out.println("Tournament dataset: " +tourney.size());
        System.out.println("Quiz dataset: " +qbr.size());
        List<Data> FirstPhase = AddTourneyDifficult(qbr,tourney);
        System.out.println("First phase cleaned dataset:" +FirstPhase.size());
        List<Data> SecondPhase = CreatePrompts(FirstPhase);

        System.out.println("Second phase of dataset: " +SecondPhase.size());
        List<Data> ThirdPhase = CleanAnswers(SecondPhase);
        System.out.println("Third phase of dataset: " +ThirdPhase.size());
        List<Data> FourthPhase = CleanQuestions(ThirdPhase);

        List<Data> FifthPhase = SetTournament(FourthPhase,tourney);
        List<Data> SixthPhase = RemoveNonAscii(FifthPhase);
        List<Data> SeventhPhase = ClearStopWords(SixthPhase);
        FinalFiltered = SeventhPhase;

        System.out.println("Creating and serializing answers at " +System.currentTimeMillis());
        AnswerWeighting.SetAnswerWeights(SeventhPhase);
        System.out.println("Done serializing at " +System.currentTimeMillis());

        long time1 = System.currentTimeMillis();
        System.out.println("Reading list: ");
        List<AnswerModel> Answers = AnswerWeighting.readObject();
        System.out.println("Done reading list, it took " +((System.currentTimeMillis()-time1)/1000) +" seconds");
        AnswerAnalysis.WeighAnswers(Answers);
        //WriteDatabaseCSV(SeventhPhase);


    }

    static List<Data> SetTournament(List<Data> qb, List<Tournaments> tourney){
        for(Data d : qb){
            int tId = d.getTournamentId();
            for(Tournaments t : tourney){
                if(t.getId()==tId){
                    d.setTournament(t.getName());
                }
            }
        }
        return qb;
    }

    static void WriteDatabaseCSV(List<Data> qb){

        String fileName = "src/main/database.csv";
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

    static void OutputQuestions(List<Data> qb){
        for(Data d : qb){
            System.out.println(d.getQuestion());
            System.out.println();
        }
    }

    static List<Data> ClearStopWords(List<Data> qb){
        for(Data d : qb){
            d.setPrompt((d.getPrompt().replaceAll("\\p{P}", "")));
            d.setAlternate((d.getAlternate().replaceAll("\\p{P}", "")));
            if(d.getPrompt().length() < 5){
                d.setPrompt("null");
            }
            if(d.getAlternate().length() < 5){
                d.setAlternate("null");
            }
        }
        return qb;
    }

    static String RemoveStopWords(String s){
        List<String> stopWords = Arrays.asList("anything","word","similar","accept","until","words","mentioned","forms","form","either","like","or","prompt","the", "to", "be", "a", "about", "above", "after", "again", "against", "all" , "am" , "an" , "and" , "any" , "are" , "aren't" , "as" , "at" , "be" , "because" , "been" , "before" , "being" , "below" , "between" , "both" , "but" , "by" , "can't" , "cannot" , "could" , "couldn't" , "did" , "didn't" , "do" , "does" , "doesn't" , "doing" , "don't" , "down" , "during" , "each" , "few" , "for" , "from" , "further" , "had" , "hadn't" , "has" , "hasn't" , "have" , "haven't" , "having" , "he" , "he'd" , "he'll" , "he's" , "her" , "here" , "here's" , "hers" , "herself" , "him" , "himself" , "his" , "how" , "how's" , "i" , "i'd" , "i'll" , "i'm" , "i've" , "if" , "in" , "into" , "is" , "isn't" , "it" , "it's" , "its" , "itself" , "let's" , "me" , "more" , "most" , "mustn't" , "my" , "myself" , "no" , "nor" , "not" , "of" , "off" , "on" , "once" , "only" , "or" , "other" , "ought" , "our" , "ours");

        List<String> allWords = new ArrayList<>(Arrays.asList(s.toLowerCase().split(" ")));
        allWords.removeAll(stopWords);
        String result = String.join(" ", allWords);
        return result;
    }

    static List<Data> RemoveNonAscii(List<Data> qb){
        for(Data d : qb){
            d.setQuestion(d.getQuestion().replaceAll("[^\\p{ASCII}]", ""));
            d.setAnswer(d.getAnswer().replaceAll("[^\\p{ASCII}]", ""));
            d.setPrompt(d.getPrompt().replaceAll("[^\\p{ASCII}]", ""));
            d.setAlternate(d.getAlternate().replaceAll("[^\\p{ASCII}]", ""));
        }
        return qb;
    }

    static List<Data> CleanQuestions(List<Data> qb){
        for(Data d : qb){
            d.setQuestion(d.getQuestion().replace("(*)",""));
            d.setQuestion(d.getQuestion().replaceAll("Â",""));
            //Remove everything btwn brackets
            d.setQuestion(d.getQuestion().replaceAll("\\([^()]*\\)",""));

        }
        return qb;
    }

    static void OutputCheck(List<AnswerWeights> ans){
        for(AnswerWeights a : ans){
            System.out.println("Answer: " +a.getAnswer() + " - " +a.getCount());
        }
    }

    static List<AnswerWeights> GetAnswerSets(List<Data> qbr){
        List<String> answers = new ArrayList<String>();
        List<AnswerWeights> weights = new ArrayList<>();
        for(Data d : qbr){
            answers.add(d.getAnswer());
        }
        Set<String> uniqueAnswers = new HashSet<String>(answers);
        for(String s : uniqueAnswers){
            AnswerWeights aw = new AnswerWeights();
            aw.setAnswer(s);
            aw.setCount(0);
            for(String ans : answers){
                if(ans.equalsIgnoreCase(s)){
                    aw.setCount(aw.getCount() + 1);
                }
            }
            weights.add(aw);
        }
        return weights;
    }

    static List<Data> CleanAnswers(List<Data> qb){
        System.out.println("Cleaning answers again");
        for(Data d : qb){
            d.setAnswer(d.getAnswer().replace("&lt;",""));
            d.setAnswer(d.getAnswer().replace("&gt;",""));
            d.setAnswer(d.getAnswer().toLowerCase());
            d.setAnswer(d.getAnswer().strip());
            if(d.getAnswer().contains("do not accept")){
                d.setAnswer(d.getAnswer().substring(0,d.getAnswer().indexOf("do not accept")));
            }
            d.setPrompt(d.getPrompt().toLowerCase());
            d.setAlternate(d.getAlternate().toLowerCase());
            if(d.getAlternate().contains("do not accept")){
                d.setAlternate(d.getAlternate().substring(0,d.getAlternate().indexOf("do not accept")));
            }
            if(d.getPrompt().contains("do not accept")){
                d.setPrompt(d.getPrompt().substring(0,d.getPrompt().indexOf("do not accept")));
            }
        }
        return qb;
    }

    static List<Data> CreatePrompts(List<Data> qb){
        System.out.println("Adding prompts and cleaning answers");
        for(Data d : qb){
            String s = d.getAnswer();
            if(s.contains("[") && s.contains("]")){
                if(s.indexOf("[") < s.indexOf("]")){
                    String answer = s.substring(0,s.indexOf("["));
                    String prompt = s.substring(s.indexOf("[")+1,s.indexOf("]"));
                    if(s.contains("(") && s.contains(")")){
                        prompt += s.substring(s.indexOf("(")+1,s.indexOf(")"));
                    }
                    d.setAnswer(answer);
                    d.setPrompt(prompt);
                }else{
                    d.setPrompt("null");
                }

            }
            else if(s.contains("(") && s.contains(")")){
                if(s.indexOf("(") < s.indexOf(")")){
                    String answer = s.substring(0,s.indexOf("("));
                    String prompt = s.substring(s.indexOf("(")+1,s.indexOf(")"));

                    d.setAnswer(answer);
                    d.setPrompt(prompt);
                }else{
                    d.setPrompt("null");
                }

            }else{
                d.setPrompt("");
            }
            d.setAnswer(d.getAnswer().replaceAll("\\([^()]*\\)",""));
            d.setAnswer(d.getAnswer().replaceAll("&lt;.*&gt;",""));
            if(d.getAnswer().contains(" or ")){
               if(d.getAnswer().indexOf("or") > d.getAnswer().indexOf("(")){
                   d.setAlternate(d.getAnswer().substring(d.getAnswer().indexOf("or"),d.getAnswer().length()));
               }
               else{
                   d.setAlternate("null");
               }
            }else{
                d.setAlternate("null");
            }
            d.setAnswer(d.getAnswer().replaceAll(" or.*",""));
            d.setAnswer(d.getAnswer().replaceAll("Â",""));
            d.setAnswer(d.getAnswer().replace("[",""));
            d.setAnswer(d.getAnswer().replace("]",""));
            d.setAnswer(d.getAnswer().strip());
            d.setAnswer(d.getAnswer().toLowerCase());
            //System.out.println("Formatted answer: " +d.getAnswer());
            //System.out.println("Answer: " +d.getAnswer());
            //System.out.println("Prompts:" +d.getPrompt());
        }
        return qb;
    }

    static List<Data> AddTourneyDifficult(List<DataRaw> qbr,List<Tournaments> tourney){
        System.out.println("Adding difficulty");
        List<Data> data = new ArrayList<Data>();
        for(DataRaw d : qbr){

            Data tempDat = new Data();

            tempDat.setAnswer(d.getAnswer());
            tempDat.setCategory(d.getCategory());
            tempDat.setId(d.getId());
            tempDat.setQuestion(d.getQuestion());
            tempDat.setSubcategory(d.getSubcategory());
            tempDat.setTournamentId(d.getTournament());

            int t = d.getTournament();
            for(Tournaments tourn : tourney){
                if(tourn.getId() == t){
                    tempDat.setDifficulty(tourn.getDifficulty());
                }
            }
            data.add(tempDat);
        }
        return data;

    }

    static List<Tournaments> GetTournamentData(){

        System.out.println("Grabbing Tournaments");

        String fileName = "src/main/tournaments.csv";
        Path myPath = Paths.get(fileName);
        try (BufferedReader br = Files.newBufferedReader(myPath,
                StandardCharsets.UTF_8)) {

            HeaderColumnNameMappingStrategy<Tournaments> strategy = new HeaderColumnNameMappingStrategy<>();
            strategy.setType(Tournaments.class);

            CsvToBean<Tournaments> csvToBean = new CsvToBeanBuilder<Tournaments>(br)
                    .withMappingStrategy(strategy)
                    .withIgnoreLeadingWhiteSpace(true)
                    .build();

            List<Tournaments> tourney = csvToBean.parse();
            return tourney;
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }
    }

    static List<DataRaw> FilterData(List<DataRaw> qbr){

        System.out.println("Filtering out quizdb");

        ArrayList<DataRaw> temp = new ArrayList<>();
        for(DataRaw d : qbr){
            temp.add(d);
        }
        for(DataRaw d : temp){
            if(d.getTournament() == 0 || d.getAnswer().isEmpty()){
                qbr.remove(d);
            }
        }
        return qbr;
    }

    static List<DataRaw> GetData(){

        System.out.println("Grabbing quizdb");

        String fileName = "src/main/qbdsMain.csv";
        Path myPath = Paths.get(fileName);
        try (BufferedReader br = Files.newBufferedReader(myPath,
                StandardCharsets.UTF_8)) {

            HeaderColumnNameMappingStrategy<DataRaw> strategy = new HeaderColumnNameMappingStrategy<>();
            strategy.setType(DataRaw.class);

            CsvToBean<DataRaw> csvToBean = new CsvToBeanBuilder<DataRaw>(br)
                    .withMappingStrategy(strategy)
                    .withIgnoreLeadingWhiteSpace(true)
                    .build();

            List<DataRaw> pre_qbr = csvToBean.parse();
            List<DataRaw> qbr = FilterData(pre_qbr);
            return qbr;
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }

    }
}


