package org.example.mysite;

import java.awt.Desktop;
import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicLong;
import java.util.Map;

import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.Aspect;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.event.ApplicationReadyEvent;
import org.springframework.context.event.EventListener;
import org.springframework.stereotype.Component;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@SpringBootApplication
class HelloSpringApplication {
    public static void main(String[] args) {
        SpringApplication.run(HelloSpringApplication.class, args);
    }
}

@Aspect
@Component
class LoggingAspect {

    private final Map<String, AtomicLong> callCounters = new ConcurrentHashMap<>();

    @Around("execution(* org.example.HelloController.*(..))")
    public Object logAndMeasure(ProceedingJoinPoint joinPoint) throws Throwable {
        String methodName = joinPoint.getSignature().getName();
        long count = callCounters
                .computeIfAbsent(methodName, k -> new AtomicLong(0))
                .incrementAndGet();

        System.out.println("┌─ Вхід у метод : " + methodName);
        System.out.println("│  Виклик №      : " + count);

        long start = System.currentTimeMillis();
        Object result;
        try {
            result = joinPoint.proceed();
        } finally {
            long elapsed = System.currentTimeMillis() - start;
            System.out.println("│  Час виконання : " + elapsed + " мс");
            System.out.println("└─ Вихід з методу: " + methodName);
            System.out.println();
        }
        return result;
    }
}

@RestController
class HelloController {
    @GetMapping("/")
    public String sayHello() {
        return "Hello Spring with AOP!";
    }

    @GetMapping("/bye")
    public String sayBye() {
        return "Goodbye Spring!";
    }
}

@Component
class BrowserLauncher {
    @EventListener(ApplicationReadyEvent.class)
    public void launchBrowser() {
        System.setProperty("java.awt.headless", "false");
        var desktop = Desktop.getDesktop();
        try {
            desktop.browse(new URI("http://localhost:8080"));
        } catch (IOException | URISyntaxException e) {
        }
    }
}