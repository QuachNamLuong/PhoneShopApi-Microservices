pipeline {

    agent any
    stages {



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
        // Clean after build
        always {
            cleanWs()
        }
    }
}