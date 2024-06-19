pipeline {

    agent any
    stages {
        stage("SCM Checkout"){
            steps{
            git 'https://github.com/QuachNamLuong/PhoneShopApi-Microservices.git'
            }
        }

        stage('Build docker down') {
            steps {
                sh 'docker compose down'
            }
        }

        stage('Build docker compose') {
            steps {
                sh 'docker compose build --no-cache'
            }
        }

        stage('Deploy docker compose') {
            steps {
                 sh 'docker compose up'
            }
        }
 
    }
    post {
        always {
            cleanWs()
        }
    }
}