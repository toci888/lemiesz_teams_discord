import axios from 'axios';

export class OpenAIClient {
    static async getResponse(prompt: string): Promise<string> {
        try {
            const response = await axios.post(
                'https://api.openai.com/v1/chat/completions',
                {
                    model: 'gpt-4',
                    messages: [{ role: 'user', content: prompt }],
                    max_tokens: 100
                },
                {
                    headers: {
                        Authorization: `Bearer ${process.env.OPENAI_API_KEY}`
                    }
                }
            );

            return response.data.choices[0].message.content;
        } catch (error) {
            console.error('Błąd podczas komunikacji z OpenAI:', error);
            return 'Niestety, coś poszło nie tak.';
        }
    }
}
