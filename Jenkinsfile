pipeline {
    agent any
    
    environment {
        COMPOSE_FILE = 'docker-compose.yml'
    }
            
        
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
        
        always {
            // Clean up any resources here if needed
        }
    }
}
