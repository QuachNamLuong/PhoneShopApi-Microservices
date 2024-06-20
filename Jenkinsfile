pipeline {
    agent any  // Runs on any available agent (node)

    environment {
        COMPOSE_FILE = 'docker-compose.yml'  // Specifies the docker-compose file to use
    }
    
    stages {
        stage('Run Docker Compose') {
            steps {
                script {
                    sh "docker-compose -f ${env.COMPOSE_FILE} build --no-cache"
                }
            }
        }
        
        stage('Deploy') {
            steps {
                script {
                    sh "docker-compose -f ${env.COMPOSE_FILE} up"
                }
            }
        }
    }
    
    post {
        success {
            echo 'Pipeline successfully executed!'
        }
        
        failure {
            echo 'Pipeline failed!'
        }
        
    }
}
