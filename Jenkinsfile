pipeline {
    agent any
    
    environment {
        COMPOSE_FILE = 'docker-compose.yml'
    }
    
    stages {
        stage('Checkout') {
            steps {
                git branch: 'master',git credentialsId: 'your-credentials-id', url: 'https://quachnamluong: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@github.com/QuachNamLuong/PhoneShopApi-Microservices.git'
            }
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
