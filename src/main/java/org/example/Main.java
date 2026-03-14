package org.example;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.*;
import java.nio.file.attribute.BasicFileAttributes;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

public class Main {
    public static void main(String[] args) {
        Map<String, Integer> count = new HashMap<>();

        Path path = Paths.get("/");
        String fileName = "result.txt";

        try {
            Files.walkFileTree(path, new FileVisitor<Path>() {
                @Override
                public FileVisitResult preVisitDirectory(Path dir, BasicFileAttributes attrs) throws IOException {
                    return FileVisitResult.CONTINUE;
                }

                @Override
                public FileVisitResult visitFile(Path file, BasicFileAttributes attrs) throws IOException {
                    String name = file.getFileName().toString();
                    int index = name.lastIndexOf(".");
                    if (index > 0 && index < name.length() - 1) {
                        String extention = name.substring(index);
                        count.put(extention, count.getOrDefault(extention, 0) + 1);
                    }
                    return FileVisitResult.CONTINUE;
                }

                @Override
                public FileVisitResult visitFileFailed(Path file, IOException exc) throws IOException {
                    return FileVisitResult.CONTINUE;
                }

                @Override
                public FileVisitResult postVisitDirectory(Path dir, IOException exc) throws IOException {
                    return FileVisitResult.CONTINUE;
                }
            });
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        List<Map.Entry<String, Integer>> top50 = count.entrySet().stream()
                .sorted(Map.Entry.<String, Integer>comparingByValue().reversed())
                .limit(50)
                .toList();

        try (BufferedWriter writer = new BufferedWriter(new FileWriter(fileName))) {
            writer.write(String.format("%-5s | %-20s | %-10s\n", "№", "Розширення", "Кількість"));
            writer.write("--------------------------------------------------\n");

            int rank = 1;
            for (Map.Entry<String, Integer> entry : top50) {
                writer.write(String.format("%-5d | %-20s | %-10d\n", rank++, entry.getKey(), entry.getValue()));
            }
        } catch (IOException e) {
            System.err.println("Помилка при записі файлу: " + e.getMessage());
        }
    }
}