import { TeamsActivityHandler, TurnContext, MessageFactory } from 'botbuilder';
import { OpenAIClient } from './openaiClient';

export class LemieszBot extends TeamsActivityHandler {
    constructor() {
        super();

        this.onMessage(async (context: TurnContext, next: () => Promise<void>) => {
            const userMessage = context.activity.text;
            const aiResponse = await OpenAIClient.getResponse(userMessage);

            await context.sendActivity(MessageFactory.text(`Lemiesz: "${aiResponse}"`));

            await next();
        });
    }
}
