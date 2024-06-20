pipeline {
    agent any  // Runs on any available agent (node)

    environment {
        COMPOSE_FILE = 'docker-compose.yml'  // Specifies the docker-compose file to use
    }

    tools {
        // Define Docker tool installation
        dockerTool 'docker'  // Refers to the Docker tool configured in Jenkins
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
