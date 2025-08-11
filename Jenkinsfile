pipeline {
    agent any
    environment {
        PATH = "/opt/homebrew/bin:/usr/local/bin:${env.PATH}"
    }
    stages {
        
        // stage('Checkout') {
        //     steps {
        //         git 'https://github.com/rezacse/MicroShop.git'
        //     }
        // }

        stage('Build Docker Compose Services') {
            steps {
                sh 'docker-compose -f docker-compose.dev.yml build'
            }
        }

        stage('Run Docker Compose Services') {
            steps {
                sh 'docker-compose up -d' // -d for detached mode
            }
        }
    }

}


// pipeline {
//     agent any

//     // environment {
//     //     DOTNET_VERSION = "8.0"
//     //     AWS_REGION     = "us-east-1"
//     //     AWS_ACCOUNT_ID = "123456789012"
//     //     ECR_REPO       = "my-dotnet-api"
//     //     IMAGE_TAG      = "${env.BUILD_NUMBER}"
//     //     SONAR_PROJECT_KEY = "your-sonarcloud-project-key"
//     //     SONAR_ORG        = "your-sonarcloud-org"
//     //     SONAR_TOKEN      = credentials('sonarcloud-token') // Jenkins credentials
//     //     AWS_CREDENTIALS  = credentials('aws-access-secret') // Jenkins credentials
//     // }

//     tools {

//     }

//     stages {
//         // stage('Checkout') {
//         //     steps {
//         //         git branch: 'main', url: 'https://github.com/your-org/your-repo.git'
//         //     }
//         // }

//         // stage('Setup .NET') {
//         //     steps {
//         //         sh "dotnet --version || wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh && bash dotnet-install.sh --version ${DOTNET_VERSION}"
//         //     }
//         // }

//         // stage('Restore & Build') {
//         //     steps {
//         //         sh "dotnet restore"
//         //         sh "dotnet build --configuration Release"
//         //     }
//         // }

//          stage('Build') {
//             steps {
//                 withDockerRegistry([credentialsId: "dockerlogin", url: ""]) {
//                     script{
//                         app = docker.build("micro-shop/auth-api")
//                     }
//                 }
//             }
//         }

//         stage('Push') {
//             steps{
//                 script {
//                     docker.withRegistry('https://476382893012.dkr.ecr.us-east-1.amazonaws.com', 'ecr:us-east-1:aws-credentials') {
//                         app.push("latest")
//                     }
//                 }
//             }
//         }

//         stage('Deployment') {
//             steps {
//                 withKubeConfig(['credentialsId': 'kubelogin']) {
//                     sh('kubectrl delete all --all -n dev-mirco-shop-api')
//                     sh('kubectl apply -f deployment.yaml --namespce=dev-mirco-shop-api')
//                 }
//             }
//         }

//         // stage('Run Tests') {
//         //     steps {
//         //         sh "dotnet test --no-build --verbosity normal"
//         //     }
//         // }



//         // stage('SonarCloud Analysis') {
//         //     steps {
//         //         withSonarQubeEnv('SonarCloud') {
//         //             sh """
//         //                 dotnet tool install --global dotnet-sonarscanner
//         //                 export PATH="$PATH:/root/.dotnet/tools"
//         //                 dotnet sonarscanner begin /k:"${SONAR_PROJECT_KEY}" /o:"${SONAR_ORG}" /d:sonar.token="${SONAR_TOKEN}" /d:sonar.host.url="https://sonarcloud.io"
//         //                 dotnet build
//         //                 dotnet sonarscanner end /d:sonar.token="${SONAR_TOKEN}"
//         //             """
//         //         }
//         //     }
//         // }

//       //   stage('Docker Build') {
//       //       steps {
//       //           sh """
//       //               aws configure set aws_access_key_id ${AWS_CREDENTIALS_USR}
//       //               aws configure set aws_secret_access_key ${AWS_CREDENTIALS_PSW}
//       //               aws configure set default.region ${AWS_REGION}
//       //               aws ecr get-login-password --region ${AWS_REGION} | docker login --username AWS --password-stdin ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com
//       //               docker build -t ${ECR_REPO}:${IMAGE_TAG} .
//       //               docker tag ${ECR_REPO}:${IMAGE_TAG} ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/${ECR_REPO}:${IMAGE_TAG}
//       //           """
//       //       }
//       //   }

//       //   stage('Push to AWS ECR') {
//       //       steps {
//       //           sh """
//       //               docker push ${AWS_ACCOUNT_ID}.dkr.ecr.${AWS_REGION}.amazonaws.com/${ECR_REPO}:${IMAGE_TAG}
//       //           """
//       //       }
//       //   }
//     }

//     post {
//         success {
//             echo "✅ Build, Test, Sonar Analysis & Push to AWS ECR completed!"
//         }
//         failure {
//             echo "❌ Pipeline failed!"
//         }
//     }
// }

// // pipeline {
// //   agent any
// //   tools { 
// //         maven 'Maven_3_9_11'  
// //     }
// //    stages{
// //     stage('CompileandRunSonarAnalysis') {
// //             steps {	
// // 		sh 'mvn clean verify sonar:sonar -Dsonar.projectKey=microshopapi -Dsonar.organization=microshop -Dsonar.host.url=https://sonarcloud.io -Dsonar.token=827947d296396193d0471b5b0f3cd2311dfe7b9a'
// // 			}
// //         } 
// //   }
// // }