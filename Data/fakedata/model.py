import pandas as pd

def load_data():
    df = pd.read_csv('data/fake_data.csv')
    return df

def get_recommendations(user_id):
    df = load_data()
    user_ratings = df[df['userid'] == user_id]
    recommendations = df[df['productid'].isin(user_ratings['productid'])]
    recommendations = recommendations[recommendations['userid'] != user_id]
    return recommendations[['productid', 'rating']].drop_duplicates().to_dict('records')
