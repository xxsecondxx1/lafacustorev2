from flask import Flask, request, jsonify
from model import get_recommendations

app = Flask(__name__)

@app.route('/recommendations', methods=['GET'])
def recommendations():
    user_id = request.args.get('userid')
    if not user_id:
        return jsonify({'error': 'Missing user id'}), 400

    try:
        user_id = int(user_id)
    except ValueError:
        return jsonify({'error': 'Invalid user id'}), 400

    recommendations = get_recommendations(user_id)
    return jsonify(recommendations)

if __name__ == '__main__':
    app.run(debug=True)
