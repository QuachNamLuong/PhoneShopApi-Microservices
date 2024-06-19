pipeline {

    agent any
    stages {
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