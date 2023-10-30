package org.via.sep3.persistentserver;

import java.io.IOException;
import java.io.UncheckedIOException;
import java.util.logging.*;

public class MyLogger {
  private static MyLogger instance;
  public static synchronized MyLogger getInstance(){
    if (instance == null){
      instance = new MyLogger();
    }
    return instance;
  }
  public void log(String who, String what){
    Logger.getLogger(who).info(what);
  }
  private MyLogger(){
    LogManager logManager = LogManager.getLogManager();
    logManager.reset();
    System.setProperty("java.util.logging.SimpleFormatter.format","%1$tF %1$tT;%4$s;%3$s;%5$s%6$s%n");
    Logger rootLogger = logManager.getLogger("");
      Handler consoleHandler = new ConsoleHandler();
      consoleHandler.setLevel(Level.ALL);
      consoleHandler.setFormatter(new SimpleFormatter());
      rootLogger.addHandler(consoleHandler);
    Handler fileHandler = null;
    try {
      fileHandler = new FileHandler("./ps.log", true);
      fileHandler.setLevel(Level.ALL);
      fileHandler.setFormatter(new SimpleFormatter());
      rootLogger.addHandler(fileHandler);
    } catch (IOException e) {
      throw new UncheckedIOException(e);
    }
    rootLogger.setLevel(Level.INFO);
    Logger.getLogger("org.hibernate.SQL").setLevel(Level.FINE);
    Logger.getLogger("org.hibernate.persister.entity.AbstractEntityPersister").setLevel(Level.FINE);
  }
}
