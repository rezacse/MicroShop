pipeline {
  agent any
  tools { 
        maven 'Maven_3_9_11'  
    }
   stages{
    stage('CompileandRunSonarAnalysis') {
            steps {	
		sh 'mvn clean verify sonar:sonar -Dsonar.projectKey=microshopapi -Dsonar.organization=mircroshop -Dsonar.host.url=https://sonarcloud.io -Dsonar.token=827947d296396193d0471b5b0f3cd2311dfe7b9a'
			}
        } 
  }
}