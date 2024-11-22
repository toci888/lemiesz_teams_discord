import express, { Request, Response } from 'express';
import { BotFrameworkAdapter } from 'botbuilder';
import { LemieszBot } from './lemieszBot';
import dotenv from 'dotenv';

// Ładowanie zmiennych środowiskowych
dotenv.config();

const app = express();
app.use(express.json());

// Adapter Bot Framework
const adapter = new BotFrameworkAdapter({
    appId: process.env.MICROSOFT_APP_ID || '',
    appPassword: process.env.MICROSOFT_APP_PASSWORD || ''
});

// Obsługa błędów adaptera
adapter.onTurnError = async (context, error) => {
    console.error(`[onTurnError]: ${error}`);
    await context.sendActivity('Wystąpił błąd. Spróbuj ponownie.');
};

// Instancja bota
const bot = new LemieszBot();

// Endpoint do obsługi wiadomości
app.post('/api/messages', (req: Request, res: Response) => {
    adapter.processActivity(req, res, async (context) => {
        await bot.run(context);
    });
});

// Start serwera
const PORT = process.env.PORT || 3978;
app.listen(PORT, () => {
    console.log(`Serwer uruchomiony na porcie ${PORT}`);
});
